using System;
using Encrydec.Ciphers;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace Encrydec
{
    public class MainWindow : Window
    {
        [UI] private Label _inputTextFieldLabel;
        [UI] private Label _outputTextFieldLabel;
        [UI] private Label _keyFieldLabel;
        [UI] private Label _workModeFieldLabel;
        [UI] private Label _cryptoAlgorithmTypeFieldLabel;
        [UI] private Button _startButton;
        [UI] private TextView _inputTextField;
        [UI] private TextView _outputTextField;
        [UI] private TextView _keyField;
        [UI] private ComboBox _workModeField;
        [UI] private ComboBox _cryptoAlgorithmTypeField;
        private IEncryptor[] _encryptors =  { new Ceasar(), new Vigner() };

        public MainWindow() : this(new Builder("MainWindow.glade")) {}

        private MainWindow(Builder builder) : base(builder.GetRawOwnedObject("MainWindow"))
        {
            builder.Autoconnect(this);

            DeleteEvent += WindowDeleteEvent;
            _startButton.Clicked += StartButtonClicked;
            _inputTextField.Buffer.Changed += InputTextFieldBufferChanged;
            _keyField.Buffer.Changed += KeyFieldBufferChanged;
            _cryptoAlgorithmTypeField.Changed += CryptoAlgorithmTypeFieldChanged;
        }

        private void WindowDeleteEvent(object sender, DeleteEventArgs a) => Application.Quit();
        private void CryptoAlgorithmTypeFieldChanged(object sender, EventArgs a) => UpdateStartButtonState();
        private void InputTextFieldBufferChanged(object sender, EventArgs a) => UpdateStartButtonState();
        private void KeyFieldBufferChanged(object sender, EventArgs a) => UpdateStartButtonState();

        private void StartButtonClicked(object sender, EventArgs a)
        {

            var encryptor = _encryptors[_cryptoAlgorithmTypeField.Active];            
            
            if (_workModeField.Active == 0)
            {
                _outputTextField.Buffer.Text = encryptor
                    .Encrypt(_inputTextField.Buffer.Text,
                    _keyField.Buffer.Text);
            }
            else
            {
                _outputTextField.Buffer.Text = encryptor
                    .Decrypt(_inputTextField.Buffer.Text,
                    _keyField.Buffer.Text);
            }
        }

        private void UpdateStartButtonState()
        {
            _startButton.Sensitive = CiphersParametersValidator.CheckMessageAndKey(_keyField.Buffer.Text,
                    _inputTextField.Buffer.Text, (CipherType)_cryptoAlgorithmTypeField.Active);

        }
    }
}