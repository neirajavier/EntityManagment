Imports System.Math
Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Data
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System
Imports System.Web.UI
Imports System.Text.RegularExpressions
Imports System.Web.UI.WebControls
Imports System.Text
Imports System.Web.UI.HtmlControls

Namespace Funciones

    Public Module globales

        Public Sub EnviarMensajeJavascript(ByVal Pagina As Page,
                                           ByVal Msj As String,
                                           Optional ByVal Caps As Boolean = True,
                                           Optional ByVal TipoMSGR As Boolean = False)

            If Caps Then
                Msj = Msj.ToUpper()
            End If

            If Msj Like "*Su Sesion Expiro*" Then
                ScriptManager.RegisterStartupScript(Pagina,
                                                    Pagina.GetType(),
                                                    Guid.NewGuid().ToString(),
                                                    String.Format("alert('{0}');", Msj),
                                                    True)
            Else
                ScriptManager.RegisterStartupScript(Pagina,
                                                    Pagina.GetType(),
                                                    Guid.NewGuid().ToString(),
                                                    String.Format("try{{EnviarMSJ('{0}');}}catch(Error){{alert('{0}')}}", Msj),
                                                    True)

                If TipoMSGR Then
                    Funciones.EjecutarFuncionJavascript(Pagina, "try{vnMensajeC.UpdatePosition(0,0);}catch(Error){}")
                End If
            End If
        End Sub

        Public Sub EjecutarFuncionJavascript(ByVal Pagina As Page,
                                                    ByVal CodigoJavascript As String)

            ScriptManager.RegisterStartupScript(Pagina,
                                                Pagina.GetType(),
                                                Guid.NewGuid().ToString(),
                                                CodigoJavascript,
                                                True)
        End Sub

        Public Function validar_Mail(ByVal sMail As String) As Boolean
            ' retorna true o false    
            Return Regex.IsMatch(sMail,
                    "^([\w-]+\.)*?[\w-]+@[\w-]+\.([\w-]+\.)*?[\w]+$")
        End Function

        Public Function ANSI2Fecha(ByVal Fecha As String) As DateTime
            Dim tmpFecha() As String = Split(Fecha, " ")
            Dim nFecha As DateTime

            Dim Año As String = tmpFecha(0).Substring(0, 4)
            Dim mes As String = tmpFecha(0).Substring(4, 2)
            Dim Dia As String = tmpFecha(0).Substring(6, 2)

            nFecha = New DateTime(Año,
                     mes,
                     Dia,
                     tmpFecha(1).Split(":")(0),
                     tmpFecha(1).Split(":")(1),
                     tmpFecha(1).Split(":")(2))

            Año = Nothing
            mes = Nothing
            Dia = Nothing

            tmpFecha = Nothing

            Return nFecha
        End Function

        Public Function ANSI2FechaShort(ByVal Fecha As String) As Date
            Dim tmpFecha() As String = Split(Fecha, " ")
            Dim nFecha As Date

            Dim Dia As String = tmpFecha(0).Substring(0, 2)
            Dim mes As String = tmpFecha(0).Substring(3, 2)
            Dim Año As String = tmpFecha(0).Substring(6, 4)

            nFecha = New Date(Año, mes, Dia)

            Año = Nothing
            mes = Nothing
            Dia = Nothing

            tmpFecha = Nothing

            Return nFecha
        End Function

        Public Function Fecha2ANSI(ByVal Fecha As DateTime,
                          Optional ByVal SoloFecha As Boolean = True) As String
            Dim tmpFecha As String = ""

            Try
                With Fecha
                    tmpFecha = .Year.ToString()

                    If .Month.ToString.Length = 1 Then
                        tmpFecha &= "0" & .Month.ToString()
                    Else
                        tmpFecha &= .Month.ToString()
                    End If

                    If .Day.ToString.Length = 1 Then
                        tmpFecha &= String.Format("0{0} ",
                                              .Day)
                    Else
                        tmpFecha &= .Day.ToString() & " "
                    End If

                    If Not SoloFecha Then
                        If .Hour.ToString.Length = 1 Then
                            tmpFecha &= String.Format("0{0}:",
                                                  .Hour)
                        Else
                            tmpFecha &= .Hour.ToString() & ":"
                        End If

                        If .Minute.ToString.Length = 1 Then
                            tmpFecha &= String.Format("0{0}:",
                                                  .Minute)
                        Else
                            tmpFecha &= .Minute.ToString() & ":"
                        End If

                        If .Second.ToString.Length = 1 Then
                            tmpFecha &= "0" & .Second.ToString()
                        Else
                            tmpFecha &= .Second.ToString()
                        End If
                    End If

                    If SoloFecha Then
                        tmpFecha = tmpFecha.Trim()
                    End If

                    Return tmpFecha
                End With
            Catch ex As Exception
                Return "null"
            End Try
        End Function

        Public Function Fecha2ANSIShort(ByVal Fecha As Date) As String
            Dim tmpFecha As String = ""

            Try
                With Fecha
                    tmpFecha = .Year.ToString()

                    If .Month.ToString.Length = 1 Then
                        tmpFecha &= "0" & .Month.ToString()
                    Else
                        tmpFecha &= .Month.ToString()
                    End If

                    If .Day.ToString.Length = 1 Then
                        tmpFecha &= String.Format("0{0} ", .Day)
                    Else
                        tmpFecha &= .Day.ToString() & " "
                    End If

                    Return tmpFecha.Trim()
                End With
            Catch ex As Exception
                Return "null"
            End Try
        End Function

        Public Function VerificaCedula(Cedula As String) As Boolean
            VerificaCedula = True
            If Len(Trim(Cedula)) <> 10 Then
                VerificaCedula = False
            End If

            If Val(Mid(Cedula, 1, 2)) > 25 Then
                VerificaCedula = False
            End If

            If Val(Mid(Cedula, 3, 1)) > 5 Then
                VerificaCedula = False
            End If

            If VerificaCedula = False Then
                'MsgBox("Cedula incorrecta ")
            Else
                Dim Total As Integer
                Dim Cifra As Integer
                Total = 0

                For a = 1 To 9

                    If (a Mod 2) = 0 Then
                        Cifra = Val(Mid(Cedula, a, 1))
                    Else
                        Cifra = Val(Mid(Cedula, a, 1)) * 2
                        If Cifra > 9 Then
                            Cifra = Cifra - 9
                        End If
                    End If
                    Total = Total + Cifra
                Next

                Cifra = Total Mod 10

                If Cifra > 0 Then
                    Cifra = 10 - Cifra
                End If

                If Cifra = Val(Mid(Cedula, 10, 1)) Then
                    VerificaCedula = True
                Else
                    'MsgBox("Numero de cedula no pasa la validacin, verifique por favor")
                    VerificaCedula = False
                End If

            End If


        End Function

        Public Function encode(ByVal str As String) As String
            Dim utf8Encoding As New System.Text.UTF8Encoding(True)
            Dim encodedString() As Byte

            encodedString = utf8Encoding.GetBytes(str)

            Return utf8Encoding.GetString(encodedString)
        End Function

        Public Function AES_Encrypt(ByVal input As String, ByVal pass As String) As String
            Dim AES As New System.Security.Cryptography.RijndaelManaged
            Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
            Dim encrypted As String = ""
            Try
                Dim hash(31) As Byte
                Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass))
                Array.Copy(temp, 0, hash, 0, 16)
                Array.Copy(temp, 0, hash, 15, 16)
                AES.Key = hash
                AES.Mode = Security.Cryptography.CipherMode.ECB
                Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
                Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(input)
                encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
                Return encrypted
            Catch ex As Exception
                Return input 'If encryption fails, return the unaltered input.
            End Try
        End Function

        'Decrypt a string with AES
        Public Function AES_Decrypt(ByVal input As String, ByVal pass As String) As String
            Dim AES As New System.Security.Cryptography.RijndaelManaged
            Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
            Dim decrypted As String = ""
            Try
                Dim hash(31) As Byte
                Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass))
                Array.Copy(temp, 0, hash, 0, 16)
                Array.Copy(temp, 0, hash, 15, 16)
                AES.Key = hash
                AES.Mode = Security.Cryptography.CipherMode.ECB
                Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
                Dim Buffer As Byte() = Convert.FromBase64String(input)
                decrypted = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
                Return decrypted
            Catch ex As Exception
                Return input 'If decryption fails, return the unaltered input.
            End Try
        End Function

        Public Function KilometrosAMillas(ByVal Kilometros As Double) As Double
            Try
                Return Round(Kilometros / 1.609344, 3)
            Catch ex As Exception
                Return Round(Kilometros, 2)
            End Try
        End Function

        Public Function MillasAKilometros(ByVal Millas As Double) As Double
            Try
                Return Round(Millas * 1.609344, 0)
            Catch ex As Exception
                Return Round(Millas, 2)
            End Try
        End Function

        Public Function MillasAMillasNauticas(ByVal Millas As Double) As Double
            Try
                Return Round(Millas * 0.868976240408186, 0)
            Catch ex As Exception
                Return Round(Millas, 2)
            End Try
        End Function

        Public Function MillasNauticasAMillas(ByVal MillasNauticas As Double) As Double
            Try
                Return Round(MillasNauticas / 0.868976240408186, 2)
            Catch ex As Exception
                Return Round(MillasNauticas, 2)
            End Try
        End Function

    End Module

End Namespace