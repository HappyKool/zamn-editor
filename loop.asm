;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;;                                                                            ;;
;;  ZAMN Infinite Loop Patch                                                  ;;
;;                                                                            ;;
;;  To be assembled an inserted with xkas v0.06 by byuu                       ;;
;;  Created for ZAMN Editor http://code.google.com/p/zamn-editor/             ;;
;;                                                         by Piranhaplant    ;;
;;                                                                            ;;
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

; This is literally the most simple patch you will ever find
; It's purpose is for creating a save state just before a level is loaded
; This way the RAM in the state can be modified to start at any level

lorom

org $8088A2
Start:
STA $1CEC
BRA Start