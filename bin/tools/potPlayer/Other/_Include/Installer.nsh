!define MAINFOLDER	"PortableApps"

;=== Program Details
Name "${APPNAME} Portable"
OutFile "..\..\..\${DLNAME}_Portable_${DLVER}_${APPLANG}.exe"
InstallDir "\${FOLDER}"
Caption "${APPNAME} Portable"
Icon "${APP}.ico"
BrandingText "Portable App"

VIProductVersion "${APPVER}"
VIAddVersionKey ProductName "${APPNAME} Portable"
VIAddVersionKey CompanyName ""
VIAddVersionKey LegalCopyright ""
VIAddVersionKey FileDescription "${APPNAME} Portable"
VIAddVersionKey FileVersion "${APPVER}"
VIAddVersionKey ProductVersion "${APPVER}"
VIAddVersionKey InternalName "${APPNAME} Portable"
VIAddVersionKey LegalTrademarks ""
VIAddVersionKey OriginalFilename "${APP}Portable.exe"
VIAddVersionKey Comments "${__DATE__} ${__TIME__}"

;=== Runtime Switches
AutoCloseWindow True
RequestExecutionLevel user

;=== Include
!include MUI.nsh
!include FileFunc.nsh
!insertmacro GetDrives
!insertmacro GetOptions
!include nsDialogs.nsh

;=== Icon & Stye ===
!define MUI_ICON "${APP}.ico"
!define MUI_WELCOMEFINISHPAGE_BITMAP "..\_Include\Installer.bmp"
!define MUI_WELCOMEPAGE_TITLE "${APPNAME} Portable"
!ifndef DESCOPTIONS
!define MUI_COMPONENTSPAGE_NODESC
!endif

;=== Pages
!ifdef INPUTBOX
	Page Custom nsDialogsPage nsDialogsPageLeave
!endif
!ifdef OPTIONS
	!insertmacro MUI_PAGE_COMPONENTS
!endif
!insertmacro MUI_PAGE_DIRECTORY
!define MUI_PAGE_CUSTOMFUNCTION_SHOW ShowInstFiles
!insertmacro MUI_PAGE_INSTFILES

!ifdef FINISHRUN
	!define MUI_FINISHPAGE_RUN
	!define MUI_FINISHPAGE_RUN_FUNCTION "Launch"
	!insertmacro MUI_PAGE_FINISH
!endif

Function ShowInstFiles
	w7tbp::Start
FunctionEnd

Var MAINPATH
Function .onInit
 	FindProcDLL::FindProc "${APP}Portable.exe"
	Pop $R0
	StrCmp $R0 "1" 0 +3
	MessageBox MB_USERICON "Please close all instances of ${APP}Portable.  The portable app can not be upgraded while it is running."
	Abort


	${GetDrives} "HDD+FDD" GetDrivesCallback
	StrCpy $INSTDIR "$MAINPATH\${FOLDER}"

!ifdef OPTIONS
	Call Init
!endif

!ifdef APPSIZE
	SectionSetSize ${main} ${APPSIZE}
!endif

!ifdef RELEASURL
	RMDir "/r" "$TEMP\${APP}PortableTemp"
	CreateDirectory "$TEMP\${APP}PortableTemp"
!endif

!ifdef MULTILANG
	Call MultiLang
!endif

!ifdef MSI
	SetOutPath "$TEMP\${APP}PortableTemp"
	File "..\_Include\MSI\Test.msi"
	nsExec::Exec `"$SYSDIR\msiexec.exe" /a "$TEMP\${APP}PortableTemp\Test.msi" TARGETDIR="$TEMP\${APP}PortableTemp\TestMSI" /qn`
IfFileExists "$TEMP\${APP}PortableTemp\TestMSI\Program Files\Test Program\readme.txt" +3
	MessageBox MB_ICONEXCLAMATION "Your system can't run msiexec to extract files from setup.$\nIf you have:$\n$SYSDIR\msiexec.exe$\nTry to disable securities."
	Abort
!endif

!ifdef 7ZA
	File "/oname=$TEMP\${APP}PortableTemp\7za.exe" "..\_Include\7-Zip\7za.exe"
!endif

!ifdef 7Z
	File "/oname=$TEMP\${APP}PortableTemp\7z.dll" "..\_Include\7-Zip\7z.dll"
	File "/oname=$TEMP\${APP}PortableTemp\7z.exe" "..\_Include\7-Zip\7z.exe"
!endif

!ifdef INNO
	File "/oname=$TEMP\${APP}PortableTemp\innounp.exe" "..\_Include\InnoUnp\innounp.exe"
!endif

!ifdef RAR
	File "/oname=$TEMP\${APP}PortableTemp\UnRAR.exe" "..\_Include\UnRAR\UnRAR.exe"
!endif

FunctionEnd

Function GetDrivesCallback
	StrCmp $8 "FDD" 0 +3
	StrCmp $9 "A:\" +4
	StrCmp $9 "B:\" +3
	IfFileExists "$9${MAINFOLDER}" 0 +2
	StrCpy $MAINPATH "$9${MAINFOLDER}"
	Push $0
FunctionEnd

Function Launch
	Exec '"$INSTDIR\${APP}Portable.exe"'
FunctionEnd

!ifdef DESCRIPTION
Function AppInfo
CreateDirectory "$INSTDIR\App\AppInfo"
WriteINIStr "$INSTDIR\App\AppInfo\appinfo.ini" "Format" "Type" "PortableApps.comFormat"
WriteINIStr "$INSTDIR\App\AppInfo\appinfo.ini" "Format" "Version" "3.0"
WriteINIStr "$INSTDIR\App\AppInfo\appinfo.ini" "Details" "Name" "${APPNAME} Portable"
WriteINIStr "$INSTDIR\App\AppInfo\appinfo.ini" "Details" "AppID" "${APP}Portable"
WriteINIStr "$INSTDIR\App\AppInfo\appinfo.ini" "Details" "Publisher" "${APPNAME} Developer"
WriteINIStr "$INSTDIR\App\AppInfo\appinfo.ini" "Details" "Homepage" ""
WriteINIStr "$INSTDIR\App\AppInfo\appinfo.ini" "Details" "Category" "Utilities"
WriteINIStr "$INSTDIR\App\AppInfo\appinfo.ini" "Details" "Description" "${DESCRIPTION}"
WriteINIStr "$INSTDIR\App\AppInfo\appinfo.ini" "Details" "Language" "${APPLANG}"
WriteINIStr "$INSTDIR\App\AppInfo\appinfo.ini" "License" "Shareable" "true"
WriteINIStr "$INSTDIR\App\AppInfo\appinfo.ini" "License" "OpenSource" "true"
WriteINIStr "$INSTDIR\App\AppInfo\appinfo.ini" "License" "Freeware" "true"
WriteINIStr "$INSTDIR\App\AppInfo\appinfo.ini" "License" "CommercialUse" "true"
WriteINIStr "$INSTDIR\App\AppInfo\appinfo.ini" "Version" "PackageVersion" "${APPVER}"
WriteINIStr "$INSTDIR\App\AppInfo\appinfo.ini" "Version" "DisplayVersion" "${DLVER}"
WriteINIStr "$INSTDIR\App\AppInfo\appinfo.ini" "Control" "Icons" "1"
WriteINIStr "$INSTDIR\App\AppInfo\appinfo.ini" "Control" "Start" "${APP}Portable.exe"
File "/oname=$INSTDIR\App\AppInfo\appicon.ico" ${APP}.ico
File "/oname=$INSTDIR\App\AppInfo\appicon_16.png" ${APP}_16.png
File "/oname=$INSTDIR\App\AppInfo\appicon_32.png" ${APP}_32.png
File "/oname=$INSTDIR\help.html" ${APP}.html
SetOutPath "$INSTDIR\Other\Help"
File /r "..\..\Other\_Include\Help\*.*"
FunctionEnd
!endif

!ifdef SOURCES
Function Sources
	SetOutPath "$INSTDIR\Other\_Include"
	File "..\..\Other\_Include\*.*"
	SetOutPath "$INSTDIR\Other\Source"
	File "_${APP}Portable.nsi"
	File "_${APP}PortableInstaller.nsi"
	File "${APP}.ico"
FunctionEnd
!endif

!ifdef SOURCES & DESCRIPTION
Function SourceInfo
	SetOutPath "$INSTDIR\Other\_Include\Help"
	File /r "..\..\Other\_Include\Help\*.*"
	SetOutPath "$INSTDIR\Other\Source"
	File "${APP}.html"
	File "${APP}_16.png"
	File "${APP}_32.png"
FunctionEnd
!endif

!ifdef RELEASURL
Function CheckConnected
	Dialer::GetConnectedState
	Pop $0
StrCmp $0 "online" +3
	MessageBox MB_USERICON "You are $0: you can't download Setup to extract files!"
	Abort
FunctionEnd
!endif
