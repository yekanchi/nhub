<Query Kind="Statements">
  <Namespace>System.Buffers</Namespace>
</Query>

int[] pooledArray = ArrayPool<int>.Shared.Rent (100);  // 100 bytes

ArrayPool<int>.Shared.Return (pooledArray);
