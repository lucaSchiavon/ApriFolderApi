Imports System.IO
Imports TetClientCallToInviaMailWs.ServiceReference1

Public Class TestWs
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Dim allegato As New SerializableAttachment()
        'allegato.FileName = "nome_file.pdf" ' Imposta il nome del file dell'allegato
        'allegato.ContentType = "application/pdf" ' Imposta il tipo di contenuto dell'allegato
        'allegato.Content = LeggiContenutoAllegato("C:\Users\Luca Schiavon\Desktop\TEST\TetClientCallToInviaMailWs\TetClientCallToInviaMailWs\test.pdf") ' Leggi il contenuto del file e assegnalo all'array di byte dell'allegato

        'Dim ws As ServiceReference1.wsSoapClient = New ServiceReference1.wsSoapClient()
        'Dim response As SendEmailResponse = ws.SendEmailWithAttachments("info@lucaschiavon.eu", "invio da ws", "questo il corpo", allegato)



        Dim ws As ServiceReference1.wsSoapClient = New ServiceReference1.wsSoapClient()
        Dim token As String = ws.GetToken("", "")
        Dim Request As New SendEmailWithAttachmentsRequest()
        Request.Token = token
        Request.SmtpClient = New SerSmtpClient()
        Request.SmtpClient.Host = "smtp.6net.it"
        Request.SmtpClient.Port = "587"
        Request.SmtpClient.CredentialsUsername = "ordini.sei@6net.it"
        Request.SmtpClient.CredentialsPassword = "6Net2017"
        Request.Email = New SerEmail()
        Request.Email.Subject = "Test da WS ..."
        Request.Email.Body = "questo il testo della mail"
        Request.Email.SenderEmail = New SerMailAddress() With {.Address = "ordini.sei@6net.it", .DisplayName = "Goojob"}
        Dim LstAddresses As List(Of SerMailAddress) = New List(Of SerMailAddress)
        LstAddresses.Add(New SerMailAddress() With {.Address = "info@lucaschiavon.eu", .DisplayName = "Luca schiavon Destinatario"})
        Request.Email.AddressesEmail = LstAddresses.ToArray
        Dim LstBcc As List(Of SerMailAddress) = New List(Of SerMailAddress)
        LstBcc.Add(New SerMailAddress() With {.Address = "lully.schiavon@gmail.com", .DisplayName = "Luca schiavon Bcc"})
        Request.Email.BccsEmail = LstBcc.ToArray

        Dim LstAttachments As List(Of SerAttachment) = New List(Of SerAttachment)
        LstAttachments.Add(New SerAttachment() With {.FileName = "primo_allegato.pdf", .ContentType = "application/pdf", .Content = LeggiContenutoAllegato("C:\Users\Luca Schiavon\Desktop\TEST\TetClientCallToInviaMailWs\TetClientCallToInviaMailWs\test.pdf")})
        LstAttachments.Add(New SerAttachment() With {.FileName = "secondoallegato.pdf", .ContentType = "application/pdf", .Content = LeggiContenutoAllegato("C:\Users\Luca Schiavon\Desktop\TEST\TetClientCallToInviaMailWs\TetClientCallToInviaMailWs\test.pdf")})
        Request.Email.Attachments = LstAttachments.ToArray

        Dim response As SendEmailWithAttachmentsResponse = ws.SendEmailWithAttachments(Request)
    End Sub

    Private Function LeggiContenutoAllegato(percorsoFile As String) As Byte()
        Dim contenutoFile As Byte()

        Try
            ' Leggi il contenuto del file come array di byte
            Using fs As New FileStream(percorsoFile, FileMode.Open, FileAccess.Read)
                Using br As New BinaryReader(fs)
                    contenutoFile = br.ReadBytes(CInt(fs.Length))
                End Using
            End Using
        Catch ex As Exception
            ' Gestisci l'eccezione se si verifica un errore durante la lettura del file
            Console.WriteLine("Errore durante la lettura del file: " & ex.Message)
            contenutoFile = Nothing
        End Try

        Return contenutoFile
    End Function


End Class