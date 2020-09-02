; **************************************************************************
; === Define constants ===
; **************************************************************************
!define /date VER	"%Y.%m.%d.%H"
; !define VER 		"0.0.0.0"	; version of launcher
!define APPNAME 	"PotPlayer"	; complete name of program
!define APP 		"PotPlayer"	; short name of program without space and accent  this one is used for the final executable an in the directory structure
!define APPEXE 		"PotPlayer.exe"	; main exe name
!define APPEXEXP 	"PotPlayerXP.exe"	; main exe XP name
!define APPDIR 		"App\PotPlayer"	; main exe relative path
!define APPEXE64 	"PotPlayer64.exe"	; main exe 64 bit name
!define APPEXEXP64 	"PotPlayerXP64.exe"	; main exe XP name
!define APPDIR64 	"App\PotPlayer64"	; main exe 64 bit relative path
!define APPSWITCH 	``	; some default Parameters

; --- Define RegKeys ---
	!define REGKEY1 "HKEY_CURRENT_USER\Software\DAUM"
; $APPDATA\Daum\PotPlayer
; **************************************************************************
; === Includes ===
; **************************************************************************
!include "..\_Include\Launcher.nsh" 
!include "LogicLib.nsh"
!include "x64.nsh"
!include "TextReplace.nsh"

; **************************************************************************
; === Set basic information ===
; **************************************************************************
Name "${APPNAME} Portable"
OutFile "..\..\..\${APP}Portable\${APP}Portable.exe"
Icon "${APP}.ico"

; **************************************************************************
; === Other Actions ===
; **************************************************************************
Function Init
IfFileExists "$EXEDIR\Data\${APP}\${APP}.ini" +3
CreateDirectory "$EXEDIR\Data\${APP}"
CopyFiles "$EXEDIR\App\DefaultData\${APP}\${APP}.ini" "$EXEDIR\Data\${APP}\${APP}.ini"
ReadINIStr $0 "$EXEDIR\Data\${APP}\${APP}.ini" "Settings" "AudioCaptureFolder"
StrCmp $0 "" 0 +2
WriteINIStr "$EXEDIR\Data\${APP}\${APP}.ini" "Settings" "AudioCaptureFolder" "$EXEDIR\Capture"
ReadINIStr $0 "$EXEDIR\Data\${APP}\${APP}.ini" "Settings" "BdaSaveFolder"
StrCmp $0 "" 0 +2
WriteINIStr "$EXEDIR\Data\${APP}\${APP}.ini" "Settings" "BdaSaveFolder" "$EXEDIR\Capture"
ReadINIStr $0 "$EXEDIR\Data\${APP}\${APP}.ini" "Settings" "BroadcastSaveFolder"
StrCmp $0 "" 0 +2
WriteINIStr "$EXEDIR\Data\${APP}\${APP}.ini" "Settings" "BroadcastSaveFolder" "$EXEDIR\Capture"
ReadINIStr $0 "$EXEDIR\Data\${APP}\${APP}.ini" "Settings" "MovieCaptureFolder"
StrCmp $0 "" 0 +2
WriteINIStr "$EXEDIR\Data\${APP}\${APP}.ini" "Settings" "MovieCaptureFolder" "$EXEDIR\Capture"
ReadINIStr $0 "$EXEDIR\Data\${APP}\${APP}.ini" "Settings" "VideoCaptureFolder"
StrCmp $0 "" 0 +2
WriteINIStr "$EXEDIR\Data\${APP}\${APP}.ini" "Settings" "VideoCaptureFolder" "$EXEDIR\Capture"
ReadINIStr $0 "$EXEDIR\Data\${APP}\${APP}.ini" "Settings" "PlayListFolder"
StrCmp $0 "" 0 +2
WriteINIStr "$EXEDIR\Data\${APP}\${APP}.ini" "Settings" "PlayListFolder" "$EXEDIR\Playlist"
WriteINIStr "$EXEDIR\Data\${APP}\${APP}.ini" "Settings" "CheckAutoUpdate" "0"

ReadINIStr $0 "$EXEDIR\Data\${APP}Portable.ini" "${APP}Portable" "LastDirectory"
StrCpy $1 $0 3
StrCpy $2 $EXEDIR 3
StrCmp $1 $2 +3
	${textreplace::ReplaceInFile} "$EXEDIR\Data\${APP}\${APP}.ini" "$EXEDIR\Data\${APP}\${APP}.ini" "$1" "$2" "" $0
	${textreplace::ReplaceInFile} "$EXEDIR\Playlist\${APP}.dpl" "$EXEDIR\Playlist\${APP}.dpl" "$1" "$2" "" $0
	${textreplace::Unload}

ReadRegStr $1 HKLM "SOFTWARE\Microsoft\Windows NT\CurrentVersion" CurrentVersion
${If} $1 == "5.1"
	StrCpy $2 "XP"
${Else}
	StrCpy $2 ""
${EndIf}
${If} ${RunningX64}
${AndIf} ${FileExists} "$EXEDIR\${APPDIR64}\${APPEXE64}"
	StrCpy $3 "64"
${Else}
	StrCpy $3 ""
${EndIf}
	StrCpy $4 "$2$3"
WriteINIStr "$EXEDIR\Data\${APP}Portable.ini" "${APP}Portable" "Windows" $4

ReadINIStr $0 "$EXEDIR\Data\${APP}Portable.ini" "${APP}Portable" "Windows"
${If} $0 == "XP64"
Rename "$EXEDIR\Data\${APP}\${APP}.ini" "$EXEDIR\${APPDIR64}\${APP}XP64.ini"
Rename "$EXEDIR\Playlist\${APP}.dpl" "$EXEDIR\Playlist\${APP}XP64.dpl"
${ElseIf} $0 == "64"
Rename "$EXEDIR\Data\${APP}\${APP}.ini" "$EXEDIR\${APPDIR64}\${APP}64.ini"
Rename "$EXEDIR\Playlist\${APP}.dpl" "$EXEDIR\Playlist\${APP}64.dpl"
${ElseIf} $0 == "XP"
Rename "$EXEDIR\Data\${APP}\${APP}.ini" "$EXEDIR\${APPDIR}\${APP}XP.ini"
Rename "$EXEDIR\Playlist\${APP}.dpl" "$EXEDIR\Playlist\${APP}XP.dpl"
${Else}
Rename "$EXEDIR\${APPDIR}\${APP}.ini" "$EXEDIR\Data\${APP}\${APP}.ini"
${EndIf}

FunctionEnd

Function Close
CreateDirectory "$EXEDIR\Data\${APP}"
Rename "$EXEDIR\${APPDIR}\Playlist" "$EXEDIR\Data\${APP}\Playlist"

ReadINIStr $0 "$EXEDIR\Data\${APP}Portable.ini" "${APP}Portable" "Windows"
${If} $0 == "XP64"
Rename "$EXEDIR\${APPDIR64}\${APP}XP64.ini" "$EXEDIR\Data\${APP}\${APP}.ini"
Rename "$EXEDIR\Playlist\${APP}XP64.dpl" "$EXEDIR\Playlist\${APP}.dpl"
${ElseIf} $0 == "64"
Rename "$EXEDIR\${APPDIR64}\${APP}64.ini" "$EXEDIR\Data\${APP}\${APP}.ini"
Rename "$EXEDIR\Playlist\${APP}64.dpl" "$EXEDIR\Playlist\${APP}.dpl"
${ElseIf} $0 == "XP"
Rename "$EXEDIR\${APPDIR}\${APP}XP.ini" "$EXEDIR\Data\${APP}\${APP}.ini"
Rename "$EXEDIR\Playlist\${APP}XP.dpl" "$EXEDIR\Playlist\${APP}.dpl"
${Else}
Rename "$EXEDIR\${APPDIR}\${APP}Mini.ini" "$EXEDIR\Data\${APP}\${APP}.ini"
${EndIf}

FunctionEnd

; **************************************************************************
; ==== Running ====
; **************************************************************************

Section "Main"

	Call CheckStart

	Call BackupLocalKeys
	Call RestorePortableKeys

	Call Init

		Call SplashLogo
		Call Launch

	Call Restore

SectionEnd

Function Restore

	Call Close


	Call BackupPortableKeys
	Call RestoreLocalKeys

FunctionEnd

; **************************************************************************
; === Run Application ===
; **************************************************************************
Function Launch
ReadINIStr $1 "$EXEDIR\Data\${APP}Portable.ini" "${APP}Portable" "Windows"
SetOutPath "$EXEDIR\${APPDIR}"
${GetParameters} $0
${If} $1 == "XP64"
ExecWait `"$EXEDIR\${APPDIR64}\${APPEXEXP64}"${APPSWITCH} $0`
${ElseIf} $1 == "64"
ExecWait `"$EXEDIR\${APPDIR64}\${APPEXE64}"${APPSWITCH} $0`
${ElseIf} $1 == "XP"
ExecWait `"$EXEDIR\${APPDIR}\${APPEXEXP}"${APPSWITCH} $0`
${Else}
ExecWait `"$EXEDIR\${APPDIR}\${APPEXE}"${APPSWITCH} $0`
${EndIf}
WriteINIStr "$EXEDIR\Data\${APP}Portable.ini" "${APP}Portable" "GoodExit" "true"
newadvsplash::stop
FunctionEnd

; **************************************************************************
; ==== Actions on Registry Keys =====
; **************************************************************************
Function BackupLocalKeys
	${registry::BackupKey} "${REGKEY1}"
FunctionEnd

Function RestorePortableKeys
${registry::RestoreKey} "$EXEDIR\Data\${APP}.reg" $R0
Sleep 200
${registry::Unload}
FunctionEnd

Function BackupPortableKeys
Delete "$EXEDIR\Data\${APP}.reg"
CreateDirectory "$EXEDIR\Data"
	${registry::SaveKey} "${REGKEY1}" "$EXEDIR\Data\${APP}.reg" "/A=1" $R0
Sleep 100
FunctionEnd

Function RestoreLocalKeys
	${registry::RestoreBackupKey} "${REGKEY1}"
${registry::Unload}
FunctionEnd
