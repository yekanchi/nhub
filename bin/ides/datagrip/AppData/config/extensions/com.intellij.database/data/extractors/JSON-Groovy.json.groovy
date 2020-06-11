/*
 * Available context bindings:
 *   COLUMNS     List<DataColumn>
 *   ROWS        Iterable<DataRow>
 *   OUT         { append() }
 *   FORMATTER   { format(row, col); formatValue(Object, col) }
 *   TRANSPOSED  Boolean
 * plus ALL_COLUMNS, TABLE, DIALECT
 *
 * where:
 *   DataRow     { rowNumber(); first(); last(); data(): List<Object>; value(column): Object }
 *   DataColumn  { columnNumber(), name() }
 */


import static com.intellij.openapi.util.text.StringUtil.escapeStringCharacters as escapeStr

NEWLINE = System.getProperty("line.separator")
INDENT = "  "

def printJSON(level, col, o) {
  switch (o) {
    case Tuple: printJSON(level, o[0], o[1]); break
    case Map:
      OUT.append("{")
      o.entrySet().eachWithIndex { entry, i ->
        OUT.append("${i > 0 ? "," : ""}$NEWLINE${INDENT * (level + 1)}")
        OUT.append("\"${escapeStr(entry.getKey().toString())}\"")
        OUT.append(": ")
        printJSON(level + 1, col, entry.getValue())
      }
      OUT.append("$NEWLINE${INDENT * level}}")
      break
    case Object[]:
    case Iterable:
      OUT.append("[")
      def plain = true
      o.eachWithIndex { item, i ->
        plain = item == null || item instanceof Number || item instanceof Boolean || item instanceof String
        if (plain) {
          OUT.append(i > 0 ? ", " : "")
        }
        else {
          OUT.append("${i > 0 ? "," : ""}$NEWLINE${INDENT * (level + 1)}")
        }
        printJSON(level + 1, col, item)
      }
      if (plain) OUT.append("]") else OUT.append("$NEWLINE${INDENT * level}]")
      break
    default:
      if (DIALECT.getDbms().isMongo()) {
        withType(o, col, { printPrimitiveValue(o, col) })
      }
      else {
        printPrimitiveValue(o, col)
      }
      break
  }
}

def printPrimitiveValue(o, col) {
  switch (o) {
    case null: OUT.append("null"); break
    case Double.NaN:
    case Double.NEGATIVE_INFINITY:
    case Double.POSITIVE_INFINITY:
      OUT.append("\"$o\"")
      break
    case Number:
      OUT.append(FORMATTER.formatValue(o, col))
      break
    case Boolean: OUT.append("$o"); break
    default:
      def str = o instanceof String ? o : FORMATTER.formatValue(o, col)
      OUT.append("\"${escapeStr(str)}\""); break
      break
  }
}

def withType(o, col, func) {
  def typeName = FORMATTER.getTypeName(o, col)
  if (typeName == "timestamp" || typeName == "regex" || typeName == "binData") {
    def jsonTypeName = typeName == "timestamp" ? "timestamp" :
                       typeName == "regex" ? "regularExpression" :
                       "binary"
    OUT.append("{\"\$$jsonTypeName\": ")
    OUT.append(FORMATTER.formatValue(o, col))
    OUT.append("}")
    return
  }
  def jsonTypeName = typeName == "objectId" ? "oid" :
                     typeName == "date" ? "date" :
                     typeName == "decimal" ? "numberDecimal" :
                     typeName == "minKey" ? "minKey" :
                     typeName == "maxKey" ? "maxKey" :
                     o == Double.NaN || o == Double.POSITIVE_INFINITY || o == Double.NEGATIVE_INFINITY ? "numberDouble" :
                     null
  if (jsonTypeName != null) OUT.append("{\"\$$jsonTypeName\": ")
  func()
  if (jsonTypeName != null) OUT.append("}")
}

printJSON(0, null, ROWS.transform { row ->
  def map = new LinkedHashMap<String, String>()
  COLUMNS.each { col ->
    if (row.hasValue(col)) {
      def val = row.value(col)
      map.put(col.name(), new Tuple(col, val))
    }
  }
  map
})