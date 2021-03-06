﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataLayer.Schema;
using GUI;

namespace GUI
{
    public partial class SimpleArgEditor : Form
    {
        public readonly BoolExpandableExpression Expression;

        public SimpleArgEditor(BoolExpandableExpression expression)
        {
            Expression = expression;
            InitializeComponent();

            typeLabel.Text = Expression.Name + ": ";

            SimpleArgManagers = new List<SimpleArgManager>();

            DialogResult = DialogResult.Cancel;
        }

        public List<SimpleArgManager> SimpleArgManagers { get; private set; }

        private void SimpleArgEditor_Shown(object sender, EventArgs e)
        {
            TableLayoutPanel.RowCount = SimpleArgManagers.Count + 1;

            foreach (var manager in SimpleArgManagers)
            {
                manager.Create(this);
            }
        }

        private void SimpleArgEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var manager in SimpleArgManagers)
            {
                manager.Dispose(this);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;

            Close();
        }
    }

    public class SimpleArgManager
    {
        public int Index { get; set; }
        public string Label { get; set; }

        public virtual void Create(SimpleArgEditor editor)
        {
            editor.TableLayoutPanel.Controls.Add(new Label
            {
                Text = Label,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleRight,
                AutoSize = true
            }, 0, Index);
        }

        public virtual void Dispose(SimpleArgEditor editor)
        {
            
        }
    }

    class SimpleEnumArgManager : SimpleArgManager
    {
        public override void Create(SimpleArgEditor editor)
        {
            base.Create(editor);

            _comboBox = new ComboBox
            {
                Dock = DockStyle.Top,
                AutoSize = true
            };

            _comboBox.Items.AddRange(GetInstances());

            _comboBox.SelectedItem = GetValue(editor.Expression);
            editor.TableLayoutPanel.Controls.Add(_comboBox, 1, Index);
        }

        public override void Dispose(SimpleArgEditor editor)
        {
            base.Dispose(editor);

            SetValue(editor.Expression, _comboBox.SelectedItem);
        }

        private ComboBox _comboBox;
        public Func<BoolExpandableExpression, object> GetValue { get; set; }
        public Action<BoolExpandableExpression, object> SetValue;
        public Func<object[]> GetInstances { get; set; }
    }

    public class SimpleStringArgManager : SimpleArgManager
    {
        public override void Create(SimpleArgEditor editor)
        {
            base.Create(editor);

            _textBox = new TextBox
            {
                Text = GetValue(editor.Expression),
                Dock = DockStyle.Top,
                AutoSize = true
            };

            editor.TableLayoutPanel.Controls.Add(_textBox, 1, Index);
        }

        public override void Dispose(SimpleArgEditor editor)
        {
            base.Dispose(editor);

            SetValue(editor.Expression, _textBox.Text);
        }

        private TextBox _textBox;
        public Func<BoolExpandableExpression, string> GetValue { get; set; }
        public Action<BoolExpandableExpression, string> SetValue { get; set; }
    }
}
