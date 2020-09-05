Write-Host "Setting Environment Variables..."

#CodeHub Path:
$env:CHUB_HOME 						= "E:\NHUB"; 
$env:PSRepoName 					= "CodeHubPSRepo";
$env:PSRepoPath 					= $env:CHUB_HOME + "\bin\libs\" + $env:PSRepoName;


## PATH:
$env:path 									+= ";$env:CHUB_HOME\bin\vsCode;";
$env:path 									+= "$env:CHUB_HOME\bin\gitscm\bin;";
$env:path 									+= "$env:CHUB_HOME\bin\compilers\node;";
$env:path 									+= "$env:CHUB_HOME\bin\compilers\node\NPMCache;";
$env:path 									+= "$env:CHUB_HOME\bin\compilers\dotnet";
$env:path 									+= "$env:CHUB_HOME\";
$env:path 									+= "$env:CHUB_HOME\";

## 	JavaScript
# 	Node.js, Typescrtipy, Angular, React:
$env:NODE_PATH 								= $env:CHUB_HOME + "\bin\compilers\node"; #node executbale path
$env:NPM_CACHE 								= $env:CHUB_HOME + "\bin\compilers\node\NPMCache";
$env:NPM_CONFIG_CACHE 						= $env:CHUB_HOME + "\bin\compilers\node\NPMCache";
$env:NPM_CONFIG_SCRIPTS_PREPEND_NODE_PATH 	= "false";


## 	python
# 	anaconda
$env:CONDA_NPY = "111";
$env:LIBRARY_BIN							= $env:CHUB_HOME + "\bin\frameworks\anaconda\Library\bin";
$env:LIBRARY_IN								= $env:CHUB_HOME + "\bin\frameworks\anaconda\Library\include";
$env:LIBRARY_LIB 							= $env:CHUB_HOME + "\bin\frameworks\anaconda\Library\lib";
$env:SCRIPTS 								= $env:CHUB_HOME + "\bin\frameworks\\anaconda\Scripts";
$env:QT_PLUGIN_PATH 						= $env:CHUB_HOME + "\bin\frameworks\anaconda\Library\plugins";

##	Java
#jdk
$env:JAVA_HOME 								= $env:CHUB_HOME + "\bin\compilers\jdk";
