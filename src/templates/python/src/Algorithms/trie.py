import typing


class Node:
	def __init__(self) -> None:
       	# Note that using a dictionary for children (as in this implementation)
       	# would not by default lexicographically sort the children, which is
       	# required by the lexicographic sorting in the Sorting section.
       	# For lexicographic sorting, we can instead use an array of Nodes.
		self.children: Dict[str, Node] = {}  # mapping from character to Node
		self.value: Optional[Any] = None

	# def find(node: Node, key: str) -> Optional[Any]:
	# 	"""Find value by key in node."""
	# 	for char in key:
	# 		if char in node.children:
	# 			node = node.children[char]
	# 		else:
	# 			return None
	# 	return node.value

	# def insert(node: Node, key: str, value: Any) -> None:
	# 	"""Insert key/value pair into node."""
	# 	for char in key:
	# 		if char not in node.children:
	# 			node.children[char] = Node()
	# 		node = node.children[char]
	# 	node.value = value

	# def delete(root: Node, key: str) -> bool:
	# 	"""
	# 	Eagerly delete the key from the trie rooted at `root`.
	# 	Return whether the trie rooted at `root` is now empty.
	# 	"""
    
	# def _delete(node: Node, key: str, d: int) -> bool:
	# 	"""
	# 	Clear the node corresponding to key[d], and delete the child key[d+1]
	# 	if that subtrie is completely empty, and return whether `node` has been
	# 	cleared.
	# 	"""
	# 	if d == len(key):
	# 		node.value = None
	# 	else:
	# 		c = key[d]
	# 		if c in node.children and _delete(node.children[c], key, d+1):
	# 			del node.children[c]
	# 	# Return whether the subtrie rooted at `node` is now completely empty
	# 	return node.value is None and len(node.children) == 0
	# 	return _delete(root, key, 0)