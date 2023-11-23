using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace EquipmentMonitoringSystem_Server
{
    public partial class ServerForm : Form
    {
        private Socket m_ServerSocket;
        private List<Socket> m_ClientSocket;        
        private byte[] szData;

        public ServerForm()
        {
            InitializeComponent();

            _Init_Server();
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {
            Top = 10;
            Left = 10;            
        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {           
            Dispose();
            //Application.Exit();
            Application.ExitThread();
            Environment.Exit(0);
        }

        private void ServerForm_Activated(object sender, EventArgs e)
        {
            SetDoubleBuffered(richTextBoxServerStatus);
            SetDoubleBuffered(richTextBoxRecvMsg);
        }

        private void SetDoubleBuffered(Control control, bool doubleBuffered = true)
        {
            PropertyInfo propertyInfo = typeof(Control).GetProperty
            (
                "DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic
            );
            propertyInfo.SetValue(control, doubleBuffered, null);
        }

        private void _Init_Server()
        {
            m_ClientSocket = new List<Socket>();            
            m_ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
            // Server IP 및 Port
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 8000);

            // Server binding
            m_ServerSocket.Bind(iPEndPoint);
            m_ServerSocket.Listen(10);

            // Socket event
            SocketAsyncEventArgs socketAsyncEventArgs = new SocketAsyncEventArgs();
            // Event 발생 시, Accept_Completed 함수 실행
            socketAsyncEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(_Accept_Completed);

            // Waiting for client connection
            m_ServerSocket.AcceptAsync(socketAsyncEventArgs);            
        }

        /*
         * Client connection acceptance callback function
         */
        private void _Accept_Completed(object sender, SocketAsyncEventArgs e)
        {
            Socket clientSocket = e.AcceptSocket;            

            // 요청 Socket을 수락 후 리스트에 추가
            m_ClientSocket.Add(clientSocket);
            
            if(m_ClientSocket != null)
            {
                DisplayText_ServerStatus("<< " + clientSocket.RemoteEndPoint.ToString() + " >>" + " is connected");

                SocketAsyncEventArgs socketAsyncEventArgs = new SocketAsyncEventArgs();                

                // 수신용 buffer 할당
                szData = new byte[1024];                
                socketAsyncEventArgs.SetBuffer(szData, 0, 1024);
                socketAsyncEventArgs.UserToken = m_ClientSocket;
                socketAsyncEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(_Receive_Completed);

                // 수락 된 Socket의 데이터 수신 대기
                clientSocket.ReceiveAsync(socketAsyncEventArgs);
            }

            e.AcceptSocket = null;
            // 요청 Socket 처리 후 다시 수락 대기
            m_ServerSocket.AcceptAsync(e);
        }

        /*
         * Data receive callback function
         */
        private void _Receive_Completed(object sender, SocketAsyncEventArgs e)
        {
            Socket clientSocket = (Socket)sender;

            try
            {
                // 해당 Socket의 접속 유무 확인 후 false면 Socket을 닫음
                if (clientSocket.Connected && e.BytesTransferred > 0)
                {
                    // Data receive                
                    byte[] szData = e.Buffer;
                    string strData = Encoding.Unicode.GetString(szData);
                    string recvMsg = strData.Replace("\0", "").Trim();
                    DisplayText("<< " + clientSocket.RemoteEndPoint.ToString() + " >>" + recvMsg);

                    // 장비 Client로부터 받은 데이터를 Monitoring client로 전송
                    for (int i = m_ClientSocket.Count - 1; i >= 0; i--)
                    {
                        Socket socket = m_ClientSocket[i];
                        try
                        {
                            socket.Send(e.Buffer);
                        }
                        catch
                        {
                            // 오류가 발생하면 전송 취소하고 리스트에서 삭제한다.
                            try
                            {
                                socket.Dispose();
                            }
                            catch (SocketException)
                            {

                            }

                            m_ClientSocket.RemoveAt(i);
                        }
                    }

                    for (int i = 0; i < szData.Length; i++)
                    {
                        szData[i] = 0;
                    }

                    e.SetBuffer(szData, 0, 1024);
                    clientSocket.ReceiveAsync(e);
                }
                else
                {
                    // Socket 재사용 유무
                    clientSocket.Disconnect(false);

                    // Client socket 리스트에서 해당 Socket 삭제
                    m_ClientSocket.Remove(clientSocket);
                    DisplayText_ServerStatus("<< " + clientSocket.RemoteEndPoint.ToString() + " >>" + " is disconnected");
                }
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }            
        }

        public void _Begin_Send(string message)
        {
            byte[] buffer = Encoding.Unicode.GetBytes(message);

            for (int i = m_ClientSocket.Count - 1; i >= 0; i--)
            {
                Socket socket = m_ClientSocket[i];
                try
                {
                    socket.Send(buffer, 0, buffer.Length, SocketFlags.None);

                    DisplayText(message);
                }
                catch (SocketException)
                {
                    
                }
            }
        }

        private void textBoxSendMsg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _Begin_Send(textBoxSendMsg.Text);

                textBoxSendMsg.Clear();
                textBoxSendMsg.Focus();
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            _Begin_Send(textBoxSendMsg.Text);

            textBoxSendMsg.Clear();
            textBoxSendMsg.Focus();
        }

        private void DisplayText_ServerStatus(string text)
        {            
            if (richTextBoxServerStatus.InvokeRequired)
            {                
                richTextBoxServerStatus.BeginInvoke(new MethodInvoker(delegate
                {
                    if (richTextBoxServerStatus.Lines.Length > 100)
                    {
                        richTextBoxServerStatus.Clear();
                    }

                    richTextBoxServerStatus.AppendText("[ " + DateTime.Now.ToString() + "] " + text + Environment.NewLine);
                    richTextBoxServerStatus.ScrollToCaret();
                }));
            }
            else
            {
                richTextBoxServerStatus.AppendText("[ " + DateTime.Now.ToString() + "] " + text + Environment.NewLine);
                richTextBoxServerStatus.ScrollToCaret();
            }
        }

        private void DisplayText(string text) 
        {            
            if (richTextBoxRecvMsg.InvokeRequired) 
            {                
                richTextBoxRecvMsg.BeginInvoke(new MethodInvoker(delegate 
                {
                    if (richTextBoxRecvMsg.Lines.Length > 100)
                    {
                        richTextBoxRecvMsg.Clear();
                    }

                    richTextBoxRecvMsg.AppendText("[ " + DateTime.Now.ToString() + "] " + text + Environment.NewLine);
                    richTextBoxRecvMsg.ScrollToCaret();
                })); 
            } 
            else
            {
                richTextBoxRecvMsg.AppendText("[ " + DateTime.Now.ToString() + "] " + text + Environment.NewLine);
                richTextBoxRecvMsg.ScrollToCaret();
            }                
        }        
    }
}
