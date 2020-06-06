<Query Kind="Program">
  <Namespace>System.Reflection.Emit</Namespace>
</Query>

void Main()
{
	MethodInfo mi = typeof (Disassembler).GetMethod (
		"ReadOperand", BindingFlags.Instance | BindingFlags.NonPublic);

	Console.WriteLine (Disassembler.Disassemble (mi));
}

public class Disassembler
{
	public static string Disassemble (MethodBase method)
		=> new Disassembler (method).Dis();

	static Dictionary<short, OpCode> _opcodes = new Dictionary<short, OpCode>();

	static Disassembler()
	{
		Dictionary<short, OpCode> opcodes = new Dictionary<short, OpCode>();
		foreach (FieldInfo fi in typeof (OpCodes).GetFields
                                 (BindingFlags.Public | BindingFlags.Static))
			if (typeof (OpCode).IsAssignableFrom (fi.FieldType))
			{
				OpCode code = (OpCode)fi.GetValue (null);   // Get field's value
				if (code.OpCodeType != OpCodeType.Nternal)
					_opcodes.Add (code.Value, code);
			}
	}

	StringBuilder _output;    // The result to which we'll keep appending
	Module _module;           // This will come in handy later
	byte[] _il;               // The raw byte code
	int _pos;                 // The position we're up to in the byte code

	Disassembler (MethodBase method)
	{
		_module = method.DeclaringType.Module;
		_il = method.GetMethodBody().GetILAsByteArray();
	}

	string Dis()
	{
		_output = new StringBuilder();
		while (_pos < _il.Length) DisassembleNextInstruction();
		return _output.ToString();
	}

	void DisassembleNextInstruction()
	{
		int opStart = _pos;

		OpCode code = ReadOpCode();
		string operand = ReadOperand (code);

		_output.AppendFormat ("IL_{0:X4}:  {1,-12} {2}",
                              opStart, code.Name, operand);
		_output.AppendLine();
	}

	OpCode ReadOpCode()
	{
		byte byteCode = _il [_pos++];
		if (_opcodes.ContainsKey (byteCode)) return _opcodes [byteCode];

		if (_pos == _il.Length) throw new Exception ("Unexpected end of IL");

		short shortCode = (short)(byteCode * 256 + _il [_pos++]);

		if (!_opcodes.ContainsKey (shortCode))
			throw new Exception ("Cannot find opcode " + shortCode);

		return _opcodes [shortCode];
	}

	string ReadOperand (OpCode c)
	{
		int operandLength =
			c.OperandType == OperandType.InlineNone
				? 0 :
			c.OperandType == OperandType.ShortInlineBrTarget ||
			c.OperandType == OperandType.ShortInlineI ||
			c.OperandType == OperandType.ShortInlineVar
				? 1 :
			c.OperandType == OperandType.InlineVar
				? 2 :
			c.OperandType == OperandType.InlineI8 ||
			c.OperandType == OperandType.InlineR
				? 8 :
			c.OperandType == OperandType.InlineSwitch
				? 4 * (BitConverter.ToInt32 (_il, _pos) + 1) :
				4;  // All others are 4 bytes

		if (_pos + operandLength > _il.Length)
			throw new Exception ("Unexpected end of IL");

		string result = FormatOperand (c, operandLength);
		if (result == null)
		{                        // Write out operand bytes in hex
			result = "";
			for (int i = 0; i < operandLength; i++)
				result += _il [_pos + i].ToString ("X2") + " ";
		}
		_pos += operandLength;
		return result;
	}

	string FormatOperand (OpCode c, int operandLength)
	{
		if (operandLength == 0) return "";

		if (operandLength == 4)
			return Get4ByteOperand (c);
		else if (c.OperandType == OperandType.ShortInlineBrTarget)
			return GetShortRelativeTarget();
		else if (c.OperandType == OperandType.InlineSwitch)
			return GetSwitchTarget (operandLength);
		else
			return null;
	}

	string Get4ByteOperand (OpCode c)
	{
		int intOp = BitConverter.ToInt32 (_il, _pos);

		switch (c.OperandType)
		{
			case OperandType.InlineTok:
			case OperandType.InlineMethod:
			case OperandType.InlineField:
			case OperandType.InlineType:
				MemberInfo mi;
				try { mi = _module.ResolveMember (intOp); }
				catch { return null; }
				if (mi == null) return null;

				if (mi.ReflectedType != null)
					return mi.ReflectedType.FullName + "." + mi.Name;
				else if (mi is Type)
					return ((Type)mi).FullName;
				else
					return mi.Name;

			case OperandType.InlineString:
				string s = _module.ResolveString (intOp);
				if (s != null) s = "'" + s + "'";
				return s;

			case OperandType.InlineBrTarget:
				return "IL_" + (_pos + intOp + 4).ToString ("X4");

			default:
				return null;
		}
	}

	string GetShortRelativeTarget()
	{
		int absoluteTarget = _pos + (sbyte)_il [_pos] + 1;
		return "IL_" + absoluteTarget.ToString ("X4");
	}

	string GetSwitchTarget (int operandLength)
	{
		int targetCount = BitConverter.ToInt32 (_il, _pos);
		string [] targets = new string [targetCount];
		for (int i = 0; i < targetCount; i++)
		{
			int ilTarget = BitConverter.ToInt32 (_il, _pos + (i + 1) * 4);
			targets [i] = "IL_" + (_pos + ilTarget + operandLength).ToString ("X4");
		}
		return "(" + string.Join (", ", targets) + ")";
	}
}