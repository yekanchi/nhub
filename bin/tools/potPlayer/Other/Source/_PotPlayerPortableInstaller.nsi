/*
https://t1.daumcdn.net/potplayer/PotPlayer/Version/Latest/PotPlayerSetup.exe
https://t1.daumcdn.net/potplayer/PotPlayer/Version/Latest/PotPlayerSetup64.exe

http://get.daum.net/PotPlayer/Codec/OpenCodecSetup.exe
http://get.daum.net/PotPlayer/Codec/OpenCodecSetup64.exe

http://t1.daumcdn.net/potplayer/beta/PotPlayerSetup.exe

*/
; !define TEST
!define RELEASURL	"https://t1.daumcdn.net/potplayer/PotPlayer/Version/Latest"
!define RELEASURL64	"https://t1.daumcdn.net/potplayer/PotPlayer/Version/Latest"
!define APPSETUP	"PotPlayerSetup.exe"
!define APPSETUP64	"PotPlayerSetup64.exe"
!define CODECURL	"http://get.daum.net/PotPlayer/Codec"
!define CODECSETUP	"OpenCodecSetup.exe"
!define CODESETUP64	"OpenCodecSetup64.exe"
!define 7Z ; Delete if setup not 7z
; !define APPSIZE	"34600" # kB
!define DLVER	"32-64-bit"
!define APPVER 	"0.0.0.0"
!define APPNAME "PotPlayer"
!define APP 	"PotPlayer"
!define DLNAME	"PotPlayer"
!define APPLANG	"Multilingual_Online"
!define FOLDER	"PotPlayerPortable"
!define FINISHRUN ; Delete if not Finish pages
!define OPTIONS ; Delete if no Components
!define SOURCES ; Delete if no Sources
; !define DESCRIPTION	"Multimedia Player (KMPlayer Reloaded)" ; Delete if no AppInfo

SetCompressor /SOLID lzma
SetCompressorDictSize 32

!include "..\_Include\Installer.nsh"
!include "LogicLib.nsh"
!include "x64.nsh"

!insertmacro MUI_LANGUAGE "English"
!insertmacro MUI_LANGUAGE "Bulgarian"
!insertmacro MUI_LANGUAGE "SimpChinese"
!insertmacro MUI_LANGUAGE "TradChinese"
!insertmacro MUI_LANGUAGE "French"
!insertmacro MUI_LANGUAGE "German"
!insertmacro MUI_LANGUAGE "Hebrew"
!insertmacro MUI_LANGUAGE "Hungarian"
!insertmacro MUI_LANGUAGE "Japanese"
!insertmacro MUI_LANGUAGE "Korean"
!insertmacro MUI_LANGUAGE "Polish"
!insertmacro MUI_LANGUAGE "Portuguese"
!insertmacro MUI_LANGUAGE "Russian"
!insertmacro MUI_LANGUAGE "Serbian"
!insertmacro MUI_LANGUAGE "Spanish"
!insertmacro MUI_LANGUAGE "Swedish"
!insertmacro MUI_LANGUAGE "Thai"
!insertmacro MUI_LANGUAGE "Turkish"
!insertmacro MUI_LANGUAGE "Ukrainian"

Section /o "${APPNAME} Portable 32 bit" x32
; SectionIn RO
DetailPrint "Installing ${APPNAME} Portable 32 bit"

${If} ${FileExists} "$EXEDIR\${APPSETUP}"
DetailPrint "Installing ${APPNAME} Portable"
	nsExec::Exec `"$TEMP\${APP}PortableTemp\7z.exe" x "$EXEDIR\${APPSETUP}" -aoa -o"$TEMP\${APP}PortableTemp\PotPlayerSetup"`
${Else}
Call CheckConnected
	inetc::get "${RELEASURL}/${APPSETUP}" "$TEMP\${APP}PortableTemp\${APPSETUP}" /END
	Pop $0
StrCmp $0 "OK" +3
	MessageBox MB_ICONEXCLAMATION "${APPSETUP} not found in $EXEDIR and download: $0"
	Abort
	nsExec::Exec `"$TEMP\${APP}PortableTemp\7z.exe" x "$TEMP\${APP}PortableTemp\${APPSETUP}" -aoa -o"$TEMP\${APP}PortableTemp\PotPlayerSetup"`
${EndIf}
DetailPrint "Installing ${APPNAME} Portable 32 bit"

	SetOutPath "$INSTDIR"
		File "..\..\..\${FOLDER}\${APP}Portable.exe"
	SetOutPath "$INSTDIR\App\DefaultData\PotPlayer"
		File "PotPlayer.ini"


	RMDir "/r" "$TEMP\${APP}PortableTemp\PotPlayerSetup\$$PLUGINSDIR"
	RMDir "/r" "$TEMP\${APP}PortableTemp\PotPlayerSetup\$$0"
	RMDir "/r" "$TEMP\${APP}PortableTemp\PotPlayerSetup\Html"
	Delete "$TEMP\${APP}PortableTemp\PotPlayerSetup\uninstall.exe.nsis"
	Delete "$TEMP\${APP}PortableTemp\PotPlayerSetup\PotPlayer.exe"
	Rename "$TEMP\${APP}PortableTemp\PotPlayerSetup\PotPlayerMini.exe" "$TEMP\${APP}PortableTemp\PotPlayerSetup\PotPlayer.exe"
	Delete "$TEMP\${APP}PortableTemp\PotPlayerSetup\PotPlayerXP.exe"
	Rename "$TEMP\${APP}PortableTemp\PotPlayerSetup\PotPlayerMiniXP.exe" "$TEMP\${APP}PortableTemp\PotPlayerSetup\PotPlayerXP.exe"

	SetOutPath "$INSTDIR\App\${APP}"
	CopyFiles /SILENT "$TEMP\${APP}PortableTemp\PotPlayerSetup\*.*" "$INSTDIR\App\${APP}\"

SectionEnd

Section /o "${APPNAME} Portable 64 bit" x64
DetailPrint "Installing ${APPNAME} Portable 64 bit"
${If} ${FileExists} "$EXEDIR\${APPSETUP64}"
	nsExec::Exec `"$TEMP\${APP}PortableTemp\7z.exe" x "$EXEDIR\${APPSETUP64}" -aoa -o"$TEMP\${APP}PortableTemp\PotPlayerSetup64"`
${Else}
Call CheckConnected
	inetc::get "${RELEASURL64}/${APPSETUP64}" "$TEMP\${APP}PortableTemp\${APPSETUP64}" /END
	Pop $0
StrCmp $0 "OK" +3
	MessageBox MB_ICONEXCLAMATION "${APPSETUP64} not found in $EXEDIR and download: $0"
	Abort
	nsExec::Exec `"$TEMP\${APP}PortableTemp\7z.exe" x "$TEMP\${APP}PortableTemp\${APPSETUP64}" -aoa -o"$TEMP\${APP}PortableTemp\PotPlayerSetup64"`
${EndIf}
DetailPrint "Installing ${APPNAME} Portable 64 bit"

	SetOutPath "$INSTDIR"
		File "..\..\..\${FOLDER}\${APP}Portable.exe"
	SetOutPath "$INSTDIR\App\DefaultData\PotPlayer"
		File "PotPlayer.ini"


	RMDir "/r" "$TEMP\${APP}PortableTemp\PotPlayerSetup64\$$PLUGINSDIR"
	RMDir "/r" "$TEMP\${APP}PortableTemp\PotPlayerSetup64\$$0"
	RMDir "/r" "$TEMP\${APP}PortableTemp\PotPlayerSetup64\Html"
	Delete "$TEMP\${APP}PortableTemp\PotPlayerSetup64\uninstall.exe.nsis"
	Delete "$TEMP\${APP}PortableTemp\PotPlayerSetup64\PotPlayer64.exe"
	Rename "$TEMP\${APP}PortableTemp\PotPlayerSetup64\PotPlayerMini64.exe" "$TEMP\${APP}PortableTemp\PotPlayerSetup64\PotPlayer64.exe"
	Delete "$TEMP\${APP}PortableTemp\PotPlayerSetup64\PotPlayerXP64.exe"
	Rename "$TEMP\${APP}PortableTemp\PotPlayerSetup64\PotPlayerMiniXP64.exe" "$TEMP\${APP}PortableTemp\PotPlayerSetup64\PotPlayerXP64.exe"

	SetOutPath "$INSTDIR\App\${APP}64"
	CopyFiles /SILENT "$TEMP\${APP}PortableTemp\PotPlayerSetup64\*.*" "$INSTDIR\App\${APP}64\"
SectionEnd

SectionGroup "Open Codec"

Section /o "32 bit" codec32
${If} ${FileExists} "$EXEDIR\${CODECSETUP}"
	nsExec::Exec `"$TEMP\${APP}PortableTemp\7z.exe" x "$EXEDIR\${CODECSETUP}" -aoa -o"$INSTDIR\App\${APP}"`
${Else}
Call CheckConnected
	inetc::get "${CODECURL}/${CODECSETUP}" "$TEMP\${APP}PortableTemp\${CODECSETUP}" /END
	Pop $0
StrCmp $0 "OK" +3
	MessageBox MB_ICONEXCLAMATION "${CODECSETUP} not found in $EXEDIR and download: $0"
	Abort
DetailPrint "Installing Open Codec 32 bit"
	nsExec::Exec `"$TEMP\${APP}PortableTemp\7z.exe" x "$TEMP\${APP}PortableTemp\${CODECSETUP}" -aoa -o"$INSTDIR\App\${APP}"`
${EndIf}
SectionEnd

Section /o "64 bit" codec64
${If} ${FileExists} "$EXEDIR\${CODESETUP64}"
	nsExec::Exec `"$TEMP\${APP}PortableTemp\7z.exe" x "$EXEDIR\${CODESETUP64}" -aoa -o"$INSTDIR\App\${APP}64"`
${Else}
Call CheckConnected
	inetc::get "${CODECURL}/${CODESETUP64}" "$TEMP\${APP}PortableTemp\${CODESETUP64}" /END
	Pop $0
StrCmp $0 "OK" +3
	MessageBox MB_ICONEXCLAMATION "${CODESETUP64} not found in $EXEDIR and download: $0"
	Abort
DetailPrint "Installing Open Codec 64 bit"
	nsExec::Exec `"$TEMP\${APP}PortableTemp\7z.exe" x "$TEMP\${APP}PortableTemp\${CODESETUP64}" -aoa -o"$INSTDIR\App\${APP}64"`
${EndIf}
	RMDir "/r" "$INSTDIR\App\${APP}\$$PLUGINSDIR"

SectionEnd

SectionGroupEnd

Function .onGUIEnd
!ifdef DESCRIPTION
Call AppInfo
!endif
!ifdef SOURCES
Call Sources
	SetOutPath "$INSTDIR\Other\_Include\7-Zip"
	File "..\_Include\7-Zip\7z.exe"
	File "..\_Include\7-Zip\7z.dll"
	SetOutPath "$INSTDIR\Other\Source"
	File "${APP}.ini"
!endif
!ifdef SOURCES & DESCRIPTION
Call SourceInfo
!endif
	RMDir "/r" "$TEMP\${APP}PortableTemp"
FunctionEnd

Function Init
SectionSetSize ${x32} 34600 # kB
SectionSetSize ${x64} 32800 # kB
SectionSetSize ${codec32} 19000 # kB
SectionSetSize ${codec64} 20170 # kB
${If} ${RunningX64}
SectionSetFlags ${x64} 1
${Else}
	SectionSetFlags ${x32} 1
${EndIf}
FunctionEnd

