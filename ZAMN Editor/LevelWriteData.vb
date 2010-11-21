Public Class LevelWriteData

    Public data As Byte()
    Public addrOffsets As Integer()

    Public Sub New(ByVal data As List(Of Byte), ByVal addrOffsets As Integer())
        Me.data = data.ToArray
        Me.addrOffsets = addrOffsets
    End Sub
End Class
