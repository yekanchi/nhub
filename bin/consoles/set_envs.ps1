Write-Host "Setting Environment Variables..."

$env:CHUB_HOME = "E:\\NHUB"; #node executbale path


## PATH:
$env:path += ";E:\NHUB\bin\vsCode;E:\NHUB\bin\gitscm\bin;E:\NHUB\bin\compilers\node;E:\NHUB\bin\compilers\node\NPMCache";


## 	JavaScript
# 	Node.js, Typescrtipy, Angular, React:
$env:NODE_PATH = "E:\\NHUB\\bin\\compilers\\node"; #node executbale path
$env:NPM_CACHE = "E:\\NHUB\\bin\\compilers\\node\\NPMCache";
$env:NPM_CONFIG_CACHE = "E:\\NHUB\\bin\\compilers\\node\\NPMCache";
$env:NPM_CONFIG_SCRIPTS_PREPEND_NODE_PATH = "false";


## 	python
# 	anaconda
$env:CONDA_NPY = "111";
$env:LIBRARY_BIN = "E:\\NHUB\\bin\\frameworks\\anaconda\\Library\\bin";
$env:LIBRARY_INC = "E:\\NHUB\\bin\\frameworks\\anaconda\\Library\\include";
$env:LIBRARY_LIB = "E:\\NHUB\\bin\\frameworks\\anaconda\\Library\\lib";
$env:SCRIPTS = "E:\\NHUB\\bin\\frameworks\\anaconda\\Scripts";
$env:QT_PLUGIN_PATH = "E:\\NHUB\\bin\\frameworks\\anaconda\\Library\\plugins";

##	Java
#jdk
$env:JAVA_HOME = "E:\\NHUB\\bin\\compilers\\jdk";
