using System.IO;
using System;
using System.Windows;
using System.Windows.Input;

namespace Telegram2
{
    public partial class MainWindow : Window
    {
        private static readonly string ChatHistoryFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Temp", "chat_history.txt"); 
        public MainWindow() 
        { 
            InitializeComponent();
            LoadChatHistory();
        }
        private void SendButton_Click(object sender, RoutedEventArgs e) 
        {
            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift)) 
            { 
                ClearChatHistory();
            }
            else 
            { 
                SendMessage(); 
            } 
        }
        private void MessageBox_KeyDown(object sender, KeyEventArgs e)
        { 
            if (e.Key == Key.Enter) 
            { 
                SendMessage(); 
            } 
        }
        private void SendMessage() 
        {
            string message = MessageBox.Text; if (!string.IsNullOrWhiteSpace(message)) 
            { 
                ChatHistory.Text += message + "\n"; SaveMessage(message); 
                MessageBox.Clear(); 
            }
        }
        private void SaveMessage(string message)
        { 
            Directory.CreateDirectory(Path.GetDirectoryName(ChatHistoryFile)); 
            File.AppendAllText(ChatHistoryFile, message + Environment.NewLine); 
        }
        private void LoadChatHistory() 
        { 
            if (File.Exists(ChatHistoryFile)) 
            { 
                ChatHistory.Text = File.ReadAllText(ChatHistoryFile); 
            } 
        }
        private void ClearChatHistory() 
        { 
            ChatHistory.Text = string.Empty; 
            File.WriteAllText(ChatHistoryFile, string.Empty); 
        }
    }
}
