using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace BooBox {

	#region InputBox return result
	/// <summary>
	/// Class used to store the result of an InputBox.Show message.
	/// </summary>
	public class InputBoxResult {
		public DialogResult ReturnCode;
		public String Text;
	}
	#endregion

	/// <summary>
	/// Summary description for InputBox.
	/// </summary>
	public class InputBox {

		#region Private Windows Contols and Constructor
		// Create a new instance of the form.
		private static Form InputDialogFrm;
		private static Label PromptLbl;
		private static Button OkCmd;
		private static Button CancelCmd;
		private static TextBox InputTxt;

		public InputBox() { }
		#endregion

		#region Private Variables
		private static String _formCaption = String.Empty;
		private static String _formPrompt = String.Empty;
		private static InputBoxResult _outputResponse = new InputBoxResult();
		private static String _defaultValue = String.Empty;
		private static int _xPos = -1;
		private static int _yPos = -1;
		#endregion

		#region Windows Form code
		private static void InitializeComponent() {
			// Create a new instance of the form.
			InputDialogFrm = new Form();
			PromptLbl = new Label();
			OkCmd = new Button();
			CancelCmd = new Button();
			InputTxt = new TextBox();
			InputDialogFrm.SuspendLayout();
			// 
			// lblPrompt
			// 
			PromptLbl.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
			PromptLbl.BackColor = SystemColors.Control;
			PromptLbl.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((Byte)(0)));
			PromptLbl.Location = new Point(12, 9);
			PromptLbl.Name = "lblPrompt";
			PromptLbl.Size = new Size(302, 82);
			PromptLbl.TabIndex = 3;
			// 
			// btnOK
			// 
			OkCmd.DialogResult = DialogResult.OK;
			OkCmd.UseVisualStyleBackColor = true;
			OkCmd.Location = new Point(326, 8);
			OkCmd.Name = "btnOK";
			OkCmd.Size = new Size(64, 24);
			OkCmd.TabIndex = 1;
			OkCmd.Text = "&OK";
			OkCmd.Click += new EventHandler(OkCmd_Click);
			// 
			// btnCancel
			// 
			CancelCmd.DialogResult = DialogResult.Cancel;
			CancelCmd.UseVisualStyleBackColor = true;
			CancelCmd.Location = new Point(326, 40);
			CancelCmd.Name = "btnCancel";
			CancelCmd.Size = new Size(64, 24);
			CancelCmd.TabIndex = 2;
			CancelCmd.Text = "&Cancel";
			CancelCmd.Click += new EventHandler(CancelCmd_Click);
			// 
			// txtInput
			// 
			InputTxt.Location = new Point(8, 100);
			InputTxt.Name = "txtInput";
			InputTxt.Size = new Size(379, 20);
			InputTxt.TabIndex = 0;
			InputTxt.Text = "";
			InputTxt.KeyDown += new KeyEventHandler(InputTxt_KeyDown);
			// 
			// InputBoxDialog
			// 
			InputDialogFrm.AutoScaleBaseSize = new Size(5, 13);
			InputDialogFrm.ClientSize = new Size(398, 128);
			InputDialogFrm.Controls.Add(InputTxt);
			InputDialogFrm.Controls.Add(CancelCmd);
			InputDialogFrm.Controls.Add(OkCmd);
			InputDialogFrm.Controls.Add(PromptLbl);
			InputDialogFrm.FormBorderStyle = FormBorderStyle.FixedDialog;
			InputDialogFrm.MaximizeBox = false;
			InputDialogFrm.MinimizeBox = false;
			InputDialogFrm.Name = "InputBoxDialog";
			InputDialogFrm.ResumeLayout(false);
		}
		#endregion

		#region Private function, InputBox Form move and change size
		static private void LoadForm() {
			OutputResponse.ReturnCode = DialogResult.Ignore;
			OutputResponse.Text = String.Empty;
			InputTxt.Text = _defaultValue;
			PromptLbl.Text = _formPrompt;
			InputDialogFrm.Text = _formCaption;
			System.Drawing.Rectangle workingRectangle = Screen.PrimaryScreen.WorkingArea;
			if (
				(_xPos >= 0 && _xPos < workingRectangle.Width - 100)
				&&
				(_yPos >= 0 && _yPos < workingRectangle.Height - 100)
			) {
				InputDialogFrm.StartPosition = FormStartPosition.Manual;
				InputDialogFrm.Location = new System.Drawing.Point(_xPos, _yPos);
			} else {
				InputDialogFrm.StartPosition = FormStartPosition.CenterScreen;
			}

			String PrompText = PromptLbl.Text;

			int n = 0;
			int Index = 0;
			while (PrompText.IndexOf("\n", Index) > -1) {
				Index = PrompText.IndexOf("\n", Index) + 1;
				n++;
			}

			if (n == 0) { n = 1; }

			System.Drawing.Point Txt = InputTxt.Location;
			Txt.Y = Txt.Y + (n * 4);
			InputTxt.Location = Txt;
			System.Drawing.Size form = InputDialogFrm.Size;
			form.Height = form.Height + (n * 4);
			InputDialogFrm.Size = form;

			InputTxt.SelectionStart = 0;
			InputTxt.SelectionLength = InputTxt.Text.Length;
			InputTxt.Focus();
		}
		#endregion

		#region Button Controls click event
		static private void OkCmd_Click(object sender, EventArgs e) {
			OutputResponse.ReturnCode = DialogResult.OK;
			OutputResponse.Text = InputTxt.Text;
			InputDialogFrm.Dispose();
		}
		static private void CancelCmd_Click(object sender, EventArgs e) {
			OutputResponse.ReturnCode = DialogResult.Cancel;
			OutputResponse.Text = String.Empty; //Clean output response
			InputDialogFrm.Dispose();
		}
		static private void InputTxt_KeyDown(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter) {
				OkCmd_Click(sender, e);
			}
		}
		#endregion

		#region Public Static Show functions
		static public InputBoxResult Show(String Prompt) {
			InitializeComponent();
			FormPrompt = Prompt;
			// Display the form as a modal dialog box.
			LoadForm();
			InputDialogFrm.ShowDialog();
			return OutputResponse;
		}
		static public InputBoxResult Show(String Prompt, String Title) {
			InitializeComponent();
			FormCaption = Title;
			FormPrompt = Prompt;
			// Display the form as a modal dialog box.
			LoadForm();
			InputDialogFrm.ShowDialog();
			return OutputResponse;
		}
		static public InputBoxResult Show(String Prompt, String Title, String Default) {
			InitializeComponent();
			FormCaption = Title;
			FormPrompt = Prompt;
			DefaultValue = Default;
			// Display the form as a modal dialog box.
			LoadForm();
			InputDialogFrm.ShowDialog();
			return OutputResponse;
		}
		static public InputBoxResult Show(String Prompt, String Title, String Default, int XPos, int YPos) {
			InitializeComponent();
			FormCaption = Title;
			FormPrompt = Prompt;
			DefaultValue = Default;
			XPosition = XPos;
			YPosition = YPos;
			// Display the form as a modal dialog box.
			LoadForm();
			InputDialogFrm.ShowDialog();
			return OutputResponse;
		}
		#endregion

		#region Private Properties
		static private String FormCaption {
			set { _formCaption = value; }
		}
		static private String FormPrompt {
			set { _formPrompt = value; }
		}
		static private InputBoxResult OutputResponse {
			get { return _outputResponse; }
			set { _outputResponse = value; }
		}
		static private String DefaultValue {
			set { _defaultValue = value; }
		}
		static private int XPosition {
			set { if (value >= 0) { _xPos = value; } }
		}
		static private int YPosition {
			set { if (value >= 0) { _yPos = value; } }
		}
		#endregion

	}
}
