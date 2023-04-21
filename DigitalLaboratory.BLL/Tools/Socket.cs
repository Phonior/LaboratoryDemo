using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DemoV4.BLL.Tools
{
    //服务器类
    public class ServerSocket
    {
        #region 自身的属性

        //开放的IP地址
        public string? ServerIP;
        //开放的端口信息
        public int ServerPort;

        //创建ServerSocket对象(TCP方式)
        public Socket server_socket = null;

        //创建被连接的ClientSocket对象
        public Socket client_socket = null;

        //连接的客户端的信息
        public string ClientSocketInfo = "";

        //是否始终监听客户端消息的开关
        public static bool IfRunListeningThread = false;

        //开启监听的线程
        Thread threadServerListen;

        //获取客户端消息的线程
        Thread threadGetClientMessage;

        #endregion

        #region 通过委托对外发送消息

        //声明第1组委托(当有Client连接成功之后)
        public delegate void ConnectWithClientSuccess(string clientInfo);
        public event ConnectWithClientSuccess OnConnectWithClientSuccess;

        //声明第2组委托(当收到Client消息之后)
        public delegate void RecieveMessageFromClientSuccess(string clientMessage);
        public event RecieveMessageFromClientSuccess OnRecieveMessageFromClientSuccess;

        //声明第3组委托(当Client断开连接之后)
        public delegate void DisconnectWithClient(string clientInfo);
        public event DisconnectWithClient OnDisconnectWithClient;

        #endregion

        //开启服务器
        public bool ServerSocketStart(string ip, int port)
        {
            ServerIP = ip;
            ServerPort = port;

            server_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //如果乱写IP地址和端口，则试图监听Server端口将直接失败，根本没有下一步的Accept方法
            try
            {
                //创建IPEP对象
                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(ServerIP), ServerPort);

                //将所创建的套接字与IPEndPoint绑定
                server_socket.Bind(ipep);

                //设置socket为收听模式
                server_socket.Listen(10);

                //放到线程中执行是为了避免界面卡死，但是BeginListen不是死循环，只执行一次
                threadServerListen = new Thread(BeginListen);
                threadServerListen.Start();

                return true;
            }
            catch (Exception Error)
            {
                //MessageBox.Show(Error.ToString());
                return false;
            }


        }

        //断开服务器(需要判断是不是客户端主动断开的)
        public void ServerSocketStop(bool IfFromClient)
        {
            IfRunListeningThread = false;

            if (IfFromClient == true)
            {
                OnDisconnectWithClient("客户端主动断开连接");
            }
            else
            {
                OnDisconnectWithClient("服务器主动断开连接");
            }

            try
            {
                //放到线程中执行是为了避免界面卡死，但是BeginListen不是死循环，只执行一次
                threadServerListen = new Thread(new ThreadStart(BeginListen));
                threadServerListen.Start();
            }
            catch { }
            try
            {
                //直接关闭就可以了
                server_socket.Close();
                client_socket.Close();

                if (threadServerListen != null)
                {
                    threadServerListen.Abort();
                    //MessageBox.Show("threadServerListen已经终止");
                }
                if (threadGetClientMessage != null)
                {
                    threadGetClientMessage.Abort();
                    //MessageBox.Show("threadGetClientMessage已经终止");
                }
            }
            catch { }

        }

        //销毁服务器
        public void ServerSocketDestroy()
        {
            try
            {
                server_socket.Close();
            }
            catch { }

            try
            {
                client_socket.Close();
            }
            catch { }

            try
            {
                server_socket.Shutdown(SocketShutdown.Both);
            }
            catch { }

            try
            {
                server_socket.Dispose();
            }
            catch { }
        }

        //在死循环中不断的监听消息
        private void BeginListen()
        {
            client_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //阻塞在这里，直到ServerSocket得到客户端的响应
            client_socket = server_socket.Accept();

            IfRunListeningThread = true;

            //得到了客户端的信息，可以放到主界面显示
            ClientSocketInfo = client_socket.RemoteEndPoint.ToString();

            //已经连接成功之后，就可以在死循环中不断获取Client的消息
            threadGetClientMessage = new Thread(RecieveMessageFromClient);
            threadGetClientMessage.Start();

            //通过委托传递出去连接上的客户端的信息
            OnConnectWithClientSuccess(ClientSocketInfo);

        }

        //发送给客户端消息
        public bool SendMessage(string str)
        {
            try
            {
                string sendstr = str;
                sendstr += "\r\n";
                byte[] buffersend = Encoding.Default.GetBytes(sendstr);

                //通过这个ClientSocket执行发送
                client_socket.Send(buffersend);

                return true;
            }
            catch
            {
                return false;
            }

        }

        //发送空消息
        public bool SendEmptyMessage()
        {
            try
            {
                byte[] buffersend = new byte[1];

                //通过这个ClientSocket执行发送
                client_socket.Send(buffersend);

                return true;

            }
            catch
            {
                return false;
            }

        }

        //接受来自客户端的消息，本身是一个死循环，如果收到则通过委托传递消息
        private void RecieveMessageFromClient()
        {
            try
            {
                while (IfRunListeningThread)
                {
                    //存放一个空字符串
                    byte[] byteData = new byte[1024];

                    this.client_socket.Receive(byteData);

                    if (byteData[0] != 0)
                    {
                        string str = System.Text.Encoding.Default.GetString(byteData);
                        OnRecieveMessageFromClientSuccess(str);
                    }
                    else
                    {
                        //客户端主动断开连接，服务器需要保持
                        ServerSocketStop(true);
                    }
                }
            }
            catch { }
        }


    }
}
