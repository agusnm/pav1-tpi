﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_Principal
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Principal))
        Me.btn_clientes = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btn_fabricas = New System.Windows.Forms.Button()
        Me.btn_productos = New System.Windows.Forms.Button()
        Me.btn_tarjetas = New System.Windows.Forms.Button()
        Me.btn_usuarios = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btn_clientes
        '
        Me.btn_clientes.BackColor = System.Drawing.Color.MediumVioletRed
        Me.btn_clientes.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_clientes.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_clientes.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.btn_clientes.Location = New System.Drawing.Point(744, 98)
        Me.btn_clientes.Name = "btn_clientes"
        Me.btn_clientes.Size = New System.Drawing.Size(183, 34)
        Me.btn_clientes.TabIndex = 2
        Me.btn_clientes.Text = "Clientes"
        Me.btn_clientes.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(794, 11)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(133, 21)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Usuario logueado"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btn_fabricas
        '
        Me.btn_fabricas.BackColor = System.Drawing.Color.MediumVioletRed
        Me.btn_fabricas.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_fabricas.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_fabricas.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.btn_fabricas.Location = New System.Drawing.Point(744, 138)
        Me.btn_fabricas.Name = "btn_fabricas"
        Me.btn_fabricas.Size = New System.Drawing.Size(183, 34)
        Me.btn_fabricas.TabIndex = 4
        Me.btn_fabricas.Text = "Fábricas"
        Me.btn_fabricas.UseVisualStyleBackColor = False
        '
        'btn_productos
        '
        Me.btn_productos.BackColor = System.Drawing.Color.MediumVioletRed
        Me.btn_productos.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_productos.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_productos.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.btn_productos.Location = New System.Drawing.Point(744, 178)
        Me.btn_productos.Name = "btn_productos"
        Me.btn_productos.Size = New System.Drawing.Size(183, 34)
        Me.btn_productos.TabIndex = 5
        Me.btn_productos.Text = "Productos"
        Me.btn_productos.UseVisualStyleBackColor = False
        '
        'btn_tarjetas
        '
        Me.btn_tarjetas.BackColor = System.Drawing.Color.MediumVioletRed
        Me.btn_tarjetas.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_tarjetas.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_tarjetas.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.btn_tarjetas.Location = New System.Drawing.Point(744, 218)
        Me.btn_tarjetas.Name = "btn_tarjetas"
        Me.btn_tarjetas.Size = New System.Drawing.Size(183, 34)
        Me.btn_tarjetas.TabIndex = 6
        Me.btn_tarjetas.Text = "Tarjetas"
        Me.btn_tarjetas.UseVisualStyleBackColor = False
        '
        'btn_usuarios
        '
        Me.btn_usuarios.BackColor = System.Drawing.Color.MediumVioletRed
        Me.btn_usuarios.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btn_usuarios.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btn_usuarios.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.btn_usuarios.Location = New System.Drawing.Point(744, 258)
        Me.btn_usuarios.Name = "btn_usuarios"
        Me.btn_usuarios.Size = New System.Drawing.Size(183, 34)
        Me.btn_usuarios.TabIndex = 7
        Me.btn_usuarios.Text = "Usuarios"
        Me.btn_usuarios.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Image = Global.PAV1_TPI.My.Resources.Resources.account_female1
        Me.Label4.Location = New System.Drawing.Point(933, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(24, 24)
        Me.Label4.TabIndex = 8
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.Image = Global.PAV1_TPI.My.Resources.Resources.clotta_nombre1
        Me.Label2.Location = New System.Drawing.Point(828, 561)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(135, 44)
        Me.Label2.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Image = Global.PAV1_TPI.My.Resources.Resources.clotta_logo
        Me.Label1.Location = New System.Drawing.Point(-181, -4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(915, 622)
        Me.Label1.TabIndex = 0
        '
        'Frm_Principal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(975, 614)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btn_usuarios)
        Me.Controls.Add(Me.btn_tarjetas)
        Me.Controls.Add(Me.btn_productos)
        Me.Controls.Add(Me.btn_fabricas)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btn_clientes)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Frm_Principal"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CLOTTA _ Principal"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents btn_clientes As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents btn_fabricas As Button
    Friend WithEvents btn_productos As Button
    Friend WithEvents btn_tarjetas As Button
    Friend WithEvents btn_usuarios As Button
    Friend WithEvents Label4 As Label
End Class
