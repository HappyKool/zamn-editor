Public Class TileSuggestList
    Public Shared TilesetAddresses As Integer() = {&HD8200, &HE38EF} ', &HD4200, &HE0200, &HDBEB5}
    Public Shared Lists As Byte()() = {My.Resources.Grass, My.Resources.Mall}
    Public Shared Data As New List(Of List(Of List(Of List(Of Byte))))
    Public Shared ConnectsTo As New List(Of List(Of List(Of List(Of Integer))))
    Public Shared AllDataLists As New List(Of List(Of List(Of Byte)))

    Public Shared Sub LoadAll()
        Dim s As IO.BinaryReader
        For Each list In Lists
            s = New IO.BinaryReader(New ByteArrayStream(list))
            Data.Add(New List(Of List(Of List(Of Byte))))
            ConnectsTo.Add(New List(Of List(Of List(Of Integer))))
            AllDataLists.Add(New List(Of List(Of Byte)))
            For dir As Integer = 0 To 3
                Data.Last.Add(New List(Of List(Of Byte)))
                ConnectsTo.Last.Add(New List(Of List(Of Integer)))
                For l As Integer = 1 To s.ReadInt32
                    Data.Last.Last.Add(New List(Of Byte))
                    ConnectsTo.Last.Last.Add(New List(Of Integer))
                    For m As Integer = 1 To s.ReadInt32
                        ConnectsTo.Last.Last.Last.Add(s.ReadInt32)
                    Next
                    For m As Integer = 1 To s.ReadInt32
                        Data.Last.Last.Last.Add(s.ReadByte)
                    Next
                    AllDataLists.Last.Add(Data.Last.Last.Last)
                Next
            Next
        Next
    End Sub

    Public Shared Function GetList(ByVal tilesetNum As Integer, ByVal startTileNum As Byte, ByVal direction As Integer) As List(Of Byte)
        Dim l As New List(Of Byte)
        If tilesetNum > -1 Then
            Dim index As Integer
            For m As Integer = 0 To Data(tilesetNum)(direction).Count - 1
                If Data(tilesetNum)(direction)(m).Contains(startTileNum) Then
                    index = m
                    Exit For
                End If
            Next
            For Each i As Integer In ConnectsTo(tilesetNum)(direction)(index)
                l.AddRange(AllDataLists(tilesetNum)(i))
            Next
        End If
        Return l
    End Function
End Class

'Hedges connect to other hedges (top and bottom)
'Hedges connect to walls (top and bottom)