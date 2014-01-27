Imports VersionOne.SDK.APIClient

Module modMain

    Sub Main()

        Try
            Dim Context As New EnvironmentContext
            Do
                Console.WriteLine("Choose API Call to Make")
                Console.WriteLine("1 = Create Defect")
                Console.WriteLine("2 = Read Defect")
                Console.WriteLine("3 = Update Defect")
                Console.WriteLine("4 = Get Defect by ID")
                Console.WriteLine("q = quit")
                Select Case Console.ReadLine()
                    Case "1"
                        AssetCreateNewAsset(Context, "New Defect From API", "Scope:1093")
                    Case "2"
                        AssetReadAsset(Context, "Defect:2194")
                    Case "3"
                        AssetUpdateAsset(Context, "Defect:2194")
                    Case "4"
                        Console.WriteLine("Enter Defect ID, Example D-01039")
                        GetDefectAsset(Context, Console.ReadLine)
                    Case "q"
                        Return
                End Select
            Loop

        Catch ex As Exception
            Console.WriteLine("Connection Error")
        End Try


    End Sub

    Function AssetCreateNewAsset(ByVal V1Context As EnvironmentContext, ByRef Title As String, ByRef Scope As String)
        Try
            'Initial Var Setup
            Dim projectId = V1Context.Services.GetOid(Scope)
            Dim assetType = V1Context.MetaModel.GetAssetType("Defect")
            Dim newDefect = V1Context.Services.[New](assetType, projectId)

            'When creating the asset one can update as many of the attributes as needed.
            Dim nameAttribute = assetType.GetAttributeDefinition("Name")
            Dim estimateAttribute = assetType.GetAttributeDefinition("Estimate")
            Dim descriptionAttribute = assetType.GetAttributeDefinition("Description")

            'Setup the actual values
            newDefect.SetAttributeValue(nameAttribute, Title)
            newDefect.SetAttributeValue(estimateAttribute, "2")
            newDefect.SetAttributeValue(descriptionAttribute, "This is the description box.")

            'Save!
            V1Context.Services.Save(newDefect, "Creation")

            Return newDefect
        Catch ex As Exception
            Console.WriteLine("Error Creating Defect")
        End Try
        Return vbNull
    End Function

    Sub AssetReadAsset(ByRef V1Context As EnvironmentContext, ByVal Token As String)
        Try
            'Initial Var Setup
            Dim assetID = V1Context.Services.GetOid(Token)
            Dim assetType = V1Context.MetaModel.GetAssetType("Defect")
            'Query Setup
            Dim query As New Query(assetID)
            Dim result As QueryResult
            Dim Defect As Asset

            'Now what do we want to see from this query
            Dim nameAttribute = assetType.GetAttributeDefinition("Name")
            Dim estimateAttribute = assetType.GetAttributeDefinition("Estimate")
            Dim descriptionAttribute = assetType.GetAttributeDefinition("Description")

            'Setup Query
            query.Selection.Add(nameAttribute)
            query.Selection.Add(estimateAttribute)
            query.Selection.Add(descriptionAttribute)

            'Run the Query
            result = V1Context.Services.Retrieve(query)

            'If Query comes back do work!
            If result.Assets.Count > 0 Then
                Defect = result.Assets(0)
                Console.WriteLine(Defect.GetAttribute(nameAttribute).Value)
                Console.WriteLine(Defect.GetAttribute(estimateAttribute).Value)
                Console.WriteLine(Defect.GetAttribute(descriptionAttribute).Value)
            End If
        Catch ex As Exception
            Console.WriteLine("Error Reading Defect")
        End Try

    End Sub

    Sub AssetUpdateAsset(ByVal V1Context As EnvironmentContext, ByRef Token As String)
        Try
            'Initial Var Setup
            Dim assetID = V1Context.Services.GetOid(Token)
            Dim assetType = V1Context.MetaModel.GetAssetType("Defect")
            'Query Var Setup
            Dim query As New Query(assetID)
            Dim result As QueryResult
            Dim Defect As Asset

            'Now what do we want to see from this query
            Dim nameAttribute = assetType.GetAttributeDefinition("Name")
            Dim estimateAttribute = assetType.GetAttributeDefinition("Estimate")
            Dim descriptionAttribute = assetType.GetAttributeDefinition("Description")

            'Setup Query
            query.Selection.Add(nameAttribute)
            query.Selection.Add(estimateAttribute)
            query.Selection.Add(descriptionAttribute)

            'Run Query
            result = V1Context.Services.Retrieve(query)

            'If Query comes back do work!
            If result.Assets.Count > 0 Then
                Defect = result.Assets(0)
                Defect.SetAttributeValue(nameAttribute, "Changed Title")
                Defect.SetAttributeValue(estimateAttribute, "5")
                Defect.SetAttributeValue(descriptionAttribute, "Updated description")
                V1Context.Services.Save(Defect, "Updated - Title, Estimate, Description")

                Console.WriteLine(Defect.GetAttribute(nameAttribute).Value)
                Console.WriteLine(Defect.GetAttribute(estimateAttribute).Value)
                Console.WriteLine(Defect.GetAttribute(descriptionAttribute).Value)
            End If
        Catch ex As Exception
            Console.WriteLine("Error Updating Defect")
        End Try




    End Sub


    Function GetDefectAsset(ByVal V1Context As EnvironmentContext, ByRef AssetID As String)
        Try
            'Initial Var Setup
            Dim Defect As Asset
            Dim assetType As IAssetType = V1Context.MetaModel.GetAssetType("Defect")
            'Query Setup
            Dim query As New Query(assetType)
            Dim queryResult As QueryResult

            'Now what do we want to see from this query
            Dim numberAttribute As IAttributeDefinition = assetType.GetAttributeDefinition("Number")
            Dim nameAttribute As IAttributeDefinition = assetType.GetAttributeDefinition("Name")

            'Setup Filter
            Dim term As New FilterTerm(numberAttribute)
            term.Equal(AssetID)

            'Setup Query
            query.Filter = term
            query.Selection.Add(nameAttribute)
            query.Selection.Add(numberAttribute)

            'Run Query
            queryResult = V1Context.Services.Retrieve(query)

            'If Query Comes Back Do Work!
            If queryResult.Assets.Count > 0 Then
                Defect = queryResult.Assets(0)

                Console.WriteLine(Defect.GetAttribute(nameAttribute).Value)
                Console.WriteLine(Defect.Oid.ToString)
                Return Defect
            Else
                Console.WriteLine("Defect ID Not Found")
            End If

        Catch ex As Exception
            Console.WriteLine("Error Finding Defect")
        End Try
        Return vbNull
    End Function


End Module
