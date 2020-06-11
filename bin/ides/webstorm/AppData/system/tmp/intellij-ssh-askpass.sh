#!/bin/sh
"E:/NHUB/bin/ides/webstorm/jbr/bin/java" -cp "E:/NHUB/bin/ides/webstorm/plugins/git4idea/lib/git4idea-rt.jar;E:/NHUB/bin/ides/webstorm/lib/xmlrpc-2.0.1.jar;E:/NHUB/bin/ides/webstorm/lib/commons-codec-1.14.jar;E:/NHUB/bin/ides/webstorm/lib/util.jar" org.jetbrains.git4idea.nativessh.GitNativeSshAskPassApp "$@"
