<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.listPlaces = New System.Windows.Forms.ListBox()
        Me.buttonEditPlace = New System.Windows.Forms.Button()
        Me.buttonNewPlace = New System.Windows.Forms.Button()
        Me.buttonNewPerson = New System.Windows.Forms.Button()
        Me.buttonEditPerson = New System.Windows.Forms.Button()
        Me.listPeople = New System.Windows.Forms.ListBox()
        Me.buttonNewThing = New System.Windows.Forms.Button()
        Me.buttonEditThing = New System.Windows.Forms.Button()
        Me.listThings = New System.Windows.Forms.ListBox()
        Me.buttonNewConnection = New System.Windows.Forms.Button()
        Me.buttonEditConnection = New System.Windows.Forms.Button()
        Me.listConnections = New System.Windows.Forms.ListBox()
        Me.buttonNewTrigger = New System.Windows.Forms.Button()
        Me.buttonEditTrigger = New System.Windows.Forms.Button()
        Me.listTriggers = New System.Windows.Forms.ListBox()
        Me.rbShowAll = New System.Windows.Forms.RadioButton()
        Me.rbFilterByPlace = New System.Windows.Forms.RadioButton()
        Me.rbFilterByPerson = New System.Windows.Forms.RadioButton()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.buttonNewPlace)
        Me.GroupBox1.Controls.Add(Me.buttonEditPlace)
        Me.GroupBox1.Controls.Add(Me.listPlaces)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(200, 229)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Places"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.buttonNewPerson)
        Me.GroupBox2.Controls.Add(Me.buttonEditPerson)
        Me.GroupBox2.Controls.Add(Me.listPeople)
        Me.GroupBox2.Location = New System.Drawing.Point(218, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(200, 229)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "People"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.buttonNewThing)
        Me.GroupBox3.Controls.Add(Me.buttonEditThing)
        Me.GroupBox3.Controls.Add(Me.listThings)
        Me.GroupBox3.Location = New System.Drawing.Point(425, 13)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(200, 229)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Things"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.buttonNewConnection)
        Me.GroupBox4.Controls.Add(Me.buttonEditConnection)
        Me.GroupBox4.Controls.Add(Me.listConnections)
        Me.GroupBox4.Location = New System.Drawing.Point(12, 247)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(200, 229)
        Me.GroupBox4.TabIndex = 1
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Connections"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.buttonNewTrigger)
        Me.GroupBox5.Controls.Add(Me.buttonEditTrigger)
        Me.GroupBox5.Controls.Add(Me.listTriggers)
        Me.GroupBox5.Location = New System.Drawing.Point(218, 247)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(200, 229)
        Me.GroupBox5.TabIndex = 1
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Triggers"
        '
        'listPlaces
        '
        Me.listPlaces.FormattingEnabled = True
        Me.listPlaces.Location = New System.Drawing.Point(7, 20)
        Me.listPlaces.Name = "listPlaces"
        Me.listPlaces.Size = New System.Drawing.Size(187, 173)
        Me.listPlaces.TabIndex = 0
        '
        'buttonEditPlace
        '
        Me.buttonEditPlace.Location = New System.Drawing.Point(119, 199)
        Me.buttonEditPlace.Name = "buttonEditPlace"
        Me.buttonEditPlace.Size = New System.Drawing.Size(75, 23)
        Me.buttonEditPlace.TabIndex = 1
        Me.buttonEditPlace.Text = "Edit Place"
        Me.buttonEditPlace.UseVisualStyleBackColor = True
        '
        'buttonNewPlace
        '
        Me.buttonNewPlace.Location = New System.Drawing.Point(38, 199)
        Me.buttonNewPlace.Name = "buttonNewPlace"
        Me.buttonNewPlace.Size = New System.Drawing.Size(75, 23)
        Me.buttonNewPlace.TabIndex = 2
        Me.buttonNewPlace.Text = "New Place"
        Me.buttonNewPlace.UseVisualStyleBackColor = True
        '
        'buttonNewPerson
        '
        Me.buttonNewPerson.Location = New System.Drawing.Point(38, 199)
        Me.buttonNewPerson.Name = "buttonNewPerson"
        Me.buttonNewPerson.Size = New System.Drawing.Size(75, 23)
        Me.buttonNewPerson.TabIndex = 5
        Me.buttonNewPerson.Text = "New Person"
        Me.buttonNewPerson.UseVisualStyleBackColor = True
        '
        'buttonEditPerson
        '
        Me.buttonEditPerson.Location = New System.Drawing.Point(119, 199)
        Me.buttonEditPerson.Name = "buttonEditPerson"
        Me.buttonEditPerson.Size = New System.Drawing.Size(75, 23)
        Me.buttonEditPerson.TabIndex = 4
        Me.buttonEditPerson.Text = "Edit Person"
        Me.buttonEditPerson.UseVisualStyleBackColor = True
        '
        'listPeople
        '
        Me.listPeople.FormattingEnabled = True
        Me.listPeople.Location = New System.Drawing.Point(7, 20)
        Me.listPeople.Name = "listPeople"
        Me.listPeople.Size = New System.Drawing.Size(187, 173)
        Me.listPeople.TabIndex = 3
        '
        'buttonNewThing
        '
        Me.buttonNewThing.Location = New System.Drawing.Point(38, 198)
        Me.buttonNewThing.Name = "buttonNewThing"
        Me.buttonNewThing.Size = New System.Drawing.Size(75, 23)
        Me.buttonNewThing.TabIndex = 5
        Me.buttonNewThing.Text = "New Thing"
        Me.buttonNewThing.UseVisualStyleBackColor = True
        '
        'buttonEditThing
        '
        Me.buttonEditThing.Location = New System.Drawing.Point(119, 198)
        Me.buttonEditThing.Name = "buttonEditThing"
        Me.buttonEditThing.Size = New System.Drawing.Size(75, 23)
        Me.buttonEditThing.TabIndex = 4
        Me.buttonEditThing.Text = "Edit Thing"
        Me.buttonEditThing.UseVisualStyleBackColor = True
        '
        'listThings
        '
        Me.listThings.FormattingEnabled = True
        Me.listThings.Location = New System.Drawing.Point(7, 19)
        Me.listThings.Name = "listThings"
        Me.listThings.Size = New System.Drawing.Size(187, 173)
        Me.listThings.TabIndex = 3
        '
        'buttonNewConnection
        '
        Me.buttonNewConnection.Location = New System.Drawing.Point(38, 198)
        Me.buttonNewConnection.Name = "buttonNewConnection"
        Me.buttonNewConnection.Size = New System.Drawing.Size(75, 23)
        Me.buttonNewConnection.TabIndex = 5
        Me.buttonNewConnection.Text = "New Conn."
        Me.buttonNewConnection.UseVisualStyleBackColor = True
        '
        'buttonEditConnection
        '
        Me.buttonEditConnection.Location = New System.Drawing.Point(119, 198)
        Me.buttonEditConnection.Name = "buttonEditConnection"
        Me.buttonEditConnection.Size = New System.Drawing.Size(75, 23)
        Me.buttonEditConnection.TabIndex = 4
        Me.buttonEditConnection.Text = "Edit Conn."
        Me.buttonEditConnection.UseVisualStyleBackColor = True
        '
        'listConnections
        '
        Me.listConnections.FormattingEnabled = True
        Me.listConnections.Location = New System.Drawing.Point(7, 19)
        Me.listConnections.Name = "listConnections"
        Me.listConnections.Size = New System.Drawing.Size(187, 173)
        Me.listConnections.TabIndex = 3
        '
        'buttonNewTrigger
        '
        Me.buttonNewTrigger.Location = New System.Drawing.Point(38, 198)
        Me.buttonNewTrigger.Name = "buttonNewTrigger"
        Me.buttonNewTrigger.Size = New System.Drawing.Size(75, 23)
        Me.buttonNewTrigger.TabIndex = 5
        Me.buttonNewTrigger.Text = "New Trigger"
        Me.buttonNewTrigger.UseVisualStyleBackColor = True
        '
        'buttonEditTrigger
        '
        Me.buttonEditTrigger.Location = New System.Drawing.Point(119, 198)
        Me.buttonEditTrigger.Name = "buttonEditTrigger"
        Me.buttonEditTrigger.Size = New System.Drawing.Size(75, 23)
        Me.buttonEditTrigger.TabIndex = 4
        Me.buttonEditTrigger.Text = "Edit Trigger"
        Me.buttonEditTrigger.UseVisualStyleBackColor = True
        '
        'listTriggers
        '
        Me.listTriggers.FormattingEnabled = True
        Me.listTriggers.Location = New System.Drawing.Point(7, 19)
        Me.listTriggers.Name = "listTriggers"
        Me.listTriggers.Size = New System.Drawing.Size(187, 173)
        Me.listTriggers.TabIndex = 3
        '
        'rbShowAll
        '
        Me.rbShowAll.AutoSize = True
        Me.rbShowAll.Checked = True
        Me.rbShowAll.Location = New System.Drawing.Point(432, 266)
        Me.rbShowAll.Name = "rbShowAll"
        Me.rbShowAll.Size = New System.Drawing.Size(66, 17)
        Me.rbShowAll.TabIndex = 3
        Me.rbShowAll.TabStop = True
        Me.rbShowAll.Text = "Show All"
        Me.rbShowAll.UseVisualStyleBackColor = True
        '
        'rbFilterByPlace
        '
        Me.rbFilterByPlace.AutoSize = True
        Me.rbFilterByPlace.Location = New System.Drawing.Point(432, 290)
        Me.rbFilterByPlace.Name = "rbFilterByPlace"
        Me.rbFilterByPlace.Size = New System.Drawing.Size(134, 17)
        Me.rbFilterByPlace.TabIndex = 4
        Me.rbFilterByPlace.Text = "Filter by selected Place"
        Me.rbFilterByPlace.UseVisualStyleBackColor = True
        '
        'rbFilterByPerson
        '
        Me.rbFilterByPerson.AutoSize = True
        Me.rbFilterByPerson.Location = New System.Drawing.Point(432, 314)
        Me.rbFilterByPerson.Name = "rbFilterByPerson"
        Me.rbFilterByPerson.Size = New System.Drawing.Size(140, 17)
        Me.rbFilterByPerson.TabIndex = 5
        Me.rbFilterByPerson.Text = "Filter by selected Person"
        Me.rbFilterByPerson.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(815, 638)
        Me.Controls.Add(Me.rbFilterByPerson)
        Me.Controls.Add(Me.rbFilterByPlace)
        Me.Controls.Add(Me.rbShowAll)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupBox1 As Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As Windows.Forms.GroupBox
    Friend WithEvents GroupBox5 As Windows.Forms.GroupBox
    Friend WithEvents buttonNewPlace As Windows.Forms.Button
    Friend WithEvents buttonEditPlace As Windows.Forms.Button
    Friend WithEvents listPlaces As Windows.Forms.ListBox
    Friend WithEvents buttonNewPerson As Windows.Forms.Button
    Friend WithEvents buttonEditPerson As Windows.Forms.Button
    Friend WithEvents listPeople As Windows.Forms.ListBox
    Friend WithEvents buttonNewThing As Windows.Forms.Button
    Friend WithEvents buttonEditThing As Windows.Forms.Button
    Friend WithEvents listThings As Windows.Forms.ListBox
    Friend WithEvents buttonNewConnection As Windows.Forms.Button
    Friend WithEvents buttonEditConnection As Windows.Forms.Button
    Friend WithEvents listConnections As Windows.Forms.ListBox
    Friend WithEvents buttonNewTrigger As Windows.Forms.Button
    Friend WithEvents buttonEditTrigger As Windows.Forms.Button
    Friend WithEvents listTriggers As Windows.Forms.ListBox
    Friend WithEvents rbShowAll As Windows.Forms.RadioButton
    Friend WithEvents rbFilterByPlace As Windows.Forms.RadioButton
    Friend WithEvents rbFilterByPerson As Windows.Forms.RadioButton
End Class
