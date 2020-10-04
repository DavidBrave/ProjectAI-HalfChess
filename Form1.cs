using Project_AI_Half_Chess.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_AI_Half_Chess
{
    public partial class Form1 : Form
    {
        Piece piece_now = new Piece();
        Board board_now = new Board();

        bool white_turn = true;
        bool still_playing = true;

        //global varible untuk resize kalo perlu
        int panel_width = 320;
        int panel_heigh = 640;

        //board untuk simpan data panel
        Board[,] boards = new Board[8, 4];
        //panel untuk ditampilkan ke layar
        Panel[,] panels = new Panel[8, 4];

        //list pergerakan semua piece
        List<Move> knight_moves = new List<Move>();
        List<Move> bishop_moves = new List<Move>();
        List<Move> rook_moves = new List<Move>();
        List<Move> queen_moves = new List<Move>();
        List<Move> king_moves = new List<Move>();

        //list untuk piece yang dimakan
        List<Piece> black_taken = new List<Piece>();
        List<Piece> white_taken = new List<Piece>();

        //biar bisa autocomplete
        string black = "Black", white = "White", green = "Green", red = "Red", none = "None";
        string king = "King", queen = "Queen", rook = "Rook", bishop = "Bishop", knight = "Knight";


        //image pieces
        Image black_king = Resources.BlackKing;
        Image black_queen = Resources.BlackQueen;
        Image black_rook = Resources.BlackRook;
        Image black_bishop = Resources.BlackBishop;
        Image black_knight = Resources.BlackKnight;
        Image black_pawn = Resources.BlackPawn;

        Image white_king = Resources.WhiteKing;
        Image white_queen = Resources.WhiteQueen;
        Image white_rook = Resources.WhiteRook;
        Image white_bishop = Resources.WhiteBishop;
        Image white_knight = Resources.WhiteKnight;
        Image white_pawn = Resources.WhitePawn;

        Image start_active = Resources.start_button_active;
        Image start_passive = Resources.start_button_passive;

        //4 kemungkinan warna board
        Color board_white = Color.GhostWhite;
        Color board_black = Color.DarkSlateGray;
        Color board_green = Color.LimeGreen;
        Color board_red = Color.Crimson;


        public Form1()
        {
            InitializeComponent();


            SetMoves();
            SetComponents();

            StartGame();
            
        }

        //procedure untuk atur component game
        private void SetComponents()
        {
            pictureBox_startGame.MouseEnter += PictureBox_startGame_MouseEnter;
            pictureBox_startGame.MouseLeave += PictureBox_startGame_MouseLeave;
            pictureBox_startGame.Click += PictureBox_startGame_Click;


            start_active = ResizeImage(start_active, 200, 50);
            start_passive = ResizeImage(start_passive, 200, 50);
            pictureBox_startGame.Width = 200;
            pictureBox_startGame.Height = 50;
            pictureBox_startGame.Location = new Point(460, 300);
            pictureBox_startGame.Image = start_passive;


            panel_blackTaken.Width = 320;
            panel_blackTaken.Height = 160;
            panel_blackTaken.Location = new Point(400, 450);

            panel_whiteTaken.Width = 320;
            panel_whiteTaken.Height = 160;
            panel_whiteTaken.Location = new Point(400, 50);
        }

        private void PictureBox_startGame_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void PictureBox_startGame_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_startGame.Image = start_passive;
        }

        private void PictureBox_startGame_MouseEnter(object sender, EventArgs e)
        {
            pictureBox_startGame.Image = start_active;
        }

        //Jangan Diubah
        //fungsi resize image
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        //cek apa game masih berlangsung
        public void CheckGame()
        {
            bool white_playing = false;
            bool black_playing = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (panels[i, j].BackgroundImage == white_king)
                    {
                        white_playing = true;
                    }
                    if (panels[i, j].BackgroundImage == black_king)
                    {
                        black_playing = true;
                    }
                }
            }


            if (white_playing == false)
            {
                still_playing = false;
                MessageBox.Show("Black Won");
            }
            if (black_playing == false)
            {
                still_playing = false;
                MessageBox.Show("White Won");
            }
        }

        //initialisai board, panel, dan piece
        public void StartGame()
        {
            
            white_turn = true;
            still_playing = true;
            piece_now = new Piece();
            board_now = new Board();

            white_taken.Clear();
            black_taken.Clear();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    boards[i, j] = new Board();
                    panels[i, j] = new Panel();

                    Board b = new Board();
                    Piece p = new Piece();
                    b.X = j;
                    b.Y = i;
                    if ((i + j) % 2 == 0)
                    {
                        b.BaseColor = black;
                    }
                    else
                    {
                        b.BaseColor = white;
                    }

                    //----------------------------------------------------\\

                    //  |♖|♔|♕|♖|
                    //  |♘|♗|♗|♘|
                    //  |  |  |  | |
                    //  |  |  |  | |
                    //  |  |  |  | |
                    //  |  |  |  | |
                    //  |  |  |  | |
                    //  |♞|♝|♝|♞|
                    //  |♜|♚|♛|♜|

                    //tentukan posisi bidak hitam

                    if ( (i == 0 && j == 0) || (i == 0 && j == 3) )
                    {
                        p.Color = black;
                        p.Type = rook;
                        p.Moves = rook_moves;
                        p.Point = 5;
                    }
                    if ((i == 0 && j == 1))
                    {
                        p.Color = black;
                        p.Type = king;
                        p.Moves = king_moves;
                        p.Point = 100000;
                    }
                    if ((i == 0 && j == 2))
                    {
                        p.Color = black;
                        p.Type = queen;
                        p.Moves = queen_moves;
                        p.Point = 9;
                    }
                    if ((i == 1 && j == 0) || (i == 1 && j == 3))
                    {
                        p.Color = black;
                        p.Type = knight;
                        p.Moves = knight_moves;
                        p.Point = 3;
                    }
                    if ((i == 1 && j == 1) || (i == 1 && j == 2))
                    {
                        p.Color = black;
                        p.Type = bishop;
                        p.Moves = bishop_moves;
                        p.Point = 3;
                    }

                    //----------------------------------------------------\\


                    //tentukan posisi bidak putih

                    if ((i == 7 && j == 0) || (i == 7 && j == 3))
                    {
                        p.Color = white;
                        p.Type = rook;
                        p.Moves = rook_moves;
                        p.Point = 5;
                    }
                    if ((i == 7 && j == 1))
                    {
                        p.Color = white;
                        p.Type = king;
                        p.Moves = king_moves;
                        p.Point = 100000;
                    }
                    if ((i == 7 && j == 2))
                    {
                        p.Color = white;
                        p.Type = queen;
                        p.Moves = queen_moves;
                        p.Point = 9;
                    }
                    if ((i == 6 && j == 0) || (i == 6 && j == 3))
                    {
                        p.Color = white;
                        p.Type = knight;
                        p.Moves = knight_moves;
                        p.Point = 3;
                    }
                    if ((i == 6 && j == 1) || (i == 6 && j == 2))
                    {
                        p.Color = white;
                        p.Type = bishop;
                        p.Moves = bishop_moves;
                        p.Point = 3;
                    }


                    b.Piece = p;
                    boards[i, j] = b;

                    Panel panel = new Panel();
                    panel.Click += ClickPanel;

                    panels[i, j] = panel;
                    
                }
            }

            ResizePanelGame();
            DrawPanelGame();
        }
        
        //jangan diubah
        //siapkan data moves seluruh bidak
        public void SetMoves()
        {
            Move m = new Move();

            //  | |X| | | |
            //  | | | | | |
            //  | | |K| | |
            //  | | | | | |
            //  | | | | | |
            m = new Move(-1, -2);
            knight_moves.Add(m);

            //  | | | | | |
            //  |X| | | | |
            //  | | |K| | |
            //  | | | | | |
            //  | | | | | |
            m = new Move(-2, -1);
            knight_moves.Add(m);

            //  | | | | | |
            //  | | | | | |
            //  | | |K| | |
            //  |X| | | | |
            //  | | | | | |
            m = new Move(-2, 1);
            knight_moves.Add(m);

            //  | | | | | |
            //  | | | | | |
            //  | | |K| | |
            //  | | | | | |
            //  | |X| | | |
            m = new Move(-1, 2);
            knight_moves.Add(m);

            //  | | | | | |
            //  | | | | | |
            //  | | |K| | |
            //  | | | | | |
            //  | | | |X| |
            m = new Move(1, 2);
            knight_moves.Add(m);

            //  | | | | | |
            //  | | | | | |
            //  | | |K| | |
            //  | | | | |X|
            //  | | | | | |
            m = new Move(2, 1);
            knight_moves.Add(m);

            //  | | | | | |
            //  | | | | |X|
            //  | | |K| | |
            //  | | | | | |
            //  | | | | | |
            m = new Move(2, -1);
            knight_moves.Add(m);

            //  | | | |X| |
            //  | | | | | |
            //  | | |K| | |
            //  | | | | | |
            //  | | | | | |
            m = new Move(1, -2);
            knight_moves.Add(m);

            //Bishop
            //Karena lebar papan = 4, diagonal maximal = 3
            for (int i = 1; i < 4; i++)
            {
                
                m = new Move(-i, -i);
                bishop_moves.Add(m);
                m = new Move(-i, i);
                bishop_moves.Add(m);
                m = new Move(i, i);
                bishop_moves.Add(m);
                m = new Move(i, -i);
                bishop_moves.Add(m);
            }

            //Rook
            //Karena lebar papan = 4, horizontal maximal = 3
            //Vertical = 7
            for (int i = 1; i < 8; i++)
            {
                m = new Move(0, i);
                rook_moves.Add(m);
                m = new Move(0, -i);
                rook_moves.Add(m);

                if (i < 4)
                {
                    m = new Move(i, 0);
                    rook_moves.Add(m);
                    m = new Move(-i, 0);
                    rook_moves.Add(m);
                }
            }

            //Queen
            //Rook + Bishop
            foreach (Move move in rook_moves)
            {
                queen_moves.Add(move);
            }
            foreach (Move move in bishop_moves)
            {
                queen_moves.Add(move);
            }

            //King
            //Queen moves, tapi cuma 1 petak
            foreach (Move move in queen_moves)
            {
                int x = move.AddX;
                int y = move.AddY;
                if (y == 1 || y == -1 || y == 0)
                {
                    if (x == 1 || x == -1 || x == 0)
                    {
                        king_moves.Add(move);
                    }
                }
            }


        }

        //ubah panel_width dan panel_height dari global
        public void ResizePanelGame()
        {
            panel_blackTaken.Controls.Clear();
            panel_whiteTaken.Controls.Clear();

            panel_game.Width = panel_width;
            panel_game.Height = panel_heigh;

            int width = panel_width / 4;
            int height = panel_heigh / 8;

            //MessageBox.Show("Width : " + width + " | " + "Height : " + height);

            black_king = ResizeImage(black_king, width, height);
            black_queen = ResizeImage(black_queen, width, height);
            black_rook = ResizeImage(black_rook, width, height);
            black_bishop = ResizeImage(black_bishop, width, height);
            black_knight = ResizeImage(black_knight, width, height);

            white_king = ResizeImage(white_king, width, height);
            white_queen = ResizeImage(white_queen, width, height);
            white_rook = ResizeImage(white_rook, width, height);
            white_bishop = ResizeImage(white_bishop, width, height);
            white_knight = ResizeImage(white_knight, width, height);


            panel_game.Controls.Clear();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    panels[i, j].Height = height;
                    panels[i, j].Width = width;
                    panels[i, j].Location = new Point(j * width, i *height);

                    panel_game.Controls.Add(panels[i, j]);
                }
            }
            
        }

        //redraw panel
        public void DrawPanelGame()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 4; j++)
                {

                    if (boards[i, j].ShownColor == none)
                    {
                        if (boards[i, j].BaseColor == white)
                        {
                            panels[i, j].BackColor = board_white;
                        }
                        else
                        {
                            panels[i, j].BackColor = board_black;
                        }
                    }
                    else
                    {
                        if (boards[i, j].ShownColor == red)
                        {
                            panels[i, j].BackColor = board_red;
                        }
                        else if (boards[i, j].ShownColor == green)
                        {
                            panels[i, j].BackColor = board_green;
                        }
                    }

                    if (boards[i, j].Piece.Color == black)
                    {
                        if (boards[i, j].Piece.Type == king)
                        {
                            panels[i, j].BackgroundImage = black_king;
                        }
                        else if (boards[i, j].Piece.Type == queen)
                        {
                            panels[i, j].BackgroundImage = black_queen;
                        }
                        else if (boards[i, j].Piece.Type == rook)
                        {
                            panels[i, j].BackgroundImage = black_rook;
                        }
                        else if (boards[i, j].Piece.Type == bishop)
                        {
                            panels[i, j].BackgroundImage = black_bishop;
                        }
                        else if (boards[i, j].Piece.Type == knight)
                        {
                            panels[i, j].BackgroundImage = black_knight;
                        }
                        else
                        {
                            panels[i, j].BackgroundImage = null;
                        }
                    }
                    else
                    {
                        if (boards[i, j].Piece.Type == king)
                        {
                            panels[i, j].BackgroundImage = white_king;
                        }
                        else if (boards[i, j].Piece.Type == queen)
                        {
                            panels[i, j].BackgroundImage = white_queen;
                        }
                        else if (boards[i, j].Piece.Type == rook)
                        {
                            panels[i, j].BackgroundImage = white_rook;
                        }
                        else if (boards[i, j].Piece.Type == bishop)
                        {
                            panels[i, j].BackgroundImage = white_bishop;
                        }
                        else if (boards[i, j].Piece.Type == knight)
                        {
                            panels[i, j].BackgroundImage = white_knight;
                        }
                        else
                        {
                            panels[i, j].BackgroundImage = null;
                        }
                    }


                }
            }


            panel_game.Invalidate();
        }

        //redraw panel makan
        public void DrawPanelTaken()
        {
            panel_blackTaken.Controls.Clear();
            panel_whiteTaken.Controls.Clear();


            for (int i = 0; i < white_taken.Count; i++)
            {
                Panel p = new Panel();
                p.Width = 80;
                p.Height = 80;

                if (white_taken[i].Type == king)
                {
                    p.BackgroundImage = white_king;
                }
                else if (white_taken[i].Type == queen)
                {
                    p.BackgroundImage = white_queen;
                }
                else if (white_taken[i].Type == rook)
                {
                    p.BackgroundImage = white_rook;
                }
                else if (white_taken[i].Type == bishop)
                {
                    p.BackgroundImage = white_bishop;
                }
                else if (white_taken[i].Type == knight)
                {
                    p.BackgroundImage = white_knight;
                }

                if (i > 3)
                {
                    p.Location = new Point((i - 4) * 80, 80);
                }
                else
                {
                    p.Location = new Point(i * 80, 0);
                }

                panel_whiteTaken.Controls.Add(p);
            }

            for (int i = 0; i < black_taken.Count; i++)
            {
                Panel p = new Panel();
                p.Width = 80;
                p.Height = 80;

                if (black_taken[i].Type == king)
                {
                    p.BackgroundImage = black_king;
                }
                else if (black_taken[i].Type == queen)
                {
                    p.BackgroundImage = black_queen;
                }
                else if (black_taken[i].Type == rook)
                {
                    p.BackgroundImage = black_rook;
                }
                else if (black_taken[i].Type == bishop)
                {
                    p.BackgroundImage = black_bishop;
                }
                else if (black_taken[i].Type == knight)
                {
                    p.BackgroundImage = black_knight;
                }

                if (i > 3)
                {
                    p.Location = new Point((i - 4) * 80, 80);
                }
                else
                {
                    p.Location = new Point(i * 80, 0);
                }

                panel_blackTaken.Controls.Add(p);
            }


            panel_blackTaken.Invalidate();
            panel_whiteTaken.Invalidate();
        }

        //cek apa ada piece yang menghalangi valid move
        public void CheckBlock(Move move, int x_now, int y_now)
        {
            int x = x_now;
            int y = y_now;

            x += move.AddX;
            y += move.AddY;

            //cek jika di block di atas
            if (x_now == x && y_now > y)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (y - i >= 0)
                    {
                        boards[y - i, x].ShownColor = none;
                    }

                }
            }
            //cek jika di block di kiri atas
            if (x_now > x && y_now > y)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (y - i >= 0 && x - i >= 0)
                    {
                        boards[y - i, x - i].ShownColor = none;
                    }

                }
            }
            //cek jika di block di kiri
            if (x_now > x && y_now == y)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (x - i >= 0)
                    {
                        boards[y, x - i].ShownColor = none;
                    }

                }
            }
            //cek jika di block di kiri bawah
            if (x_now > x && y_now < y)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (y + i < 8 && x - i >= 0)
                    {
                        boards[y + i, x - i].ShownColor = none;
                    }

                }
            }
            //cek jika di block di bawah
            if (x_now == x && y_now < y)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (y + i < 8)
                    {
                        boards[y + i, x].ShownColor = none;
                    }

                }
            }
            //cek jika di block di kanan bawah
            if (x_now < x && y_now < y)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (y + i < 8 && x + i < 4)
                    {
                        boards[y + i, x + i].ShownColor = none;
                    }

                }
            }
            //cek jika di block di kanan
            if (x_now < x && y_now == y)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (x + i < 4)
                    {
                        boards[y, x + i].ShownColor = none;
                    }

                }
            }
            //cek jika di block di kanan atas
            if (x_now < x && y_now > y)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (y - i >= 0 && x + i < 4)
                    {
                        boards[y - i, x + i].ShownColor = none;
                    }

                }
            }
        }

        //code saat panel di click
        public void ClickPanel(object sender, EventArgs e)
        {
            //kalo masih main, bisa click papan
            if (still_playing)
            {
                //cek x dan y dari petak yang ditekan
                int x_now = -1, y_now = -1;
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (panels[i, j] == (Panel)sender)
                        {
                            x_now = j; y_now = i;
                        }
                    }
                }

                //cek warna dari piece, ambil warna lawannya
                //kenapa tidak pake != ?
                //karena null dianggap warna sama c#
                string opposite_color = boards[y_now, x_now].Piece.Color;
                if (opposite_color == black)
                {
                    opposite_color = white;
                }
                else
                {
                    opposite_color = black;
                }


                //kalo click petak kosong, bersihkan semua possible move
                if (boards[y_now, x_now].ShownColor == none)
                {
                    //bershikan seluruh petak
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            boards[i, j].ShownColor = none;
                        }
                    }

                    if ((white_turn == true && boards[y_now, x_now].Piece.Color == white) ||
                        (white_turn == false && boards[y_now, x_now].Piece.Color == black))
                    {

                        //kalo petak kosong ada piecenya, show semua possible move untuk piece di petak itu
                        if (boards[y_now, x_now].Piece.Type != null)
                        {
                            //ambil data piece yang ingin dijalankan sekarang
                            piece_now = boards[y_now, x_now].Piece;
                            //ambil data board yang sedang ditekan
                            board_now = boards[y_now, x_now];

                            //loop seluruh moves dari piece saat ini
                            foreach (Move move in boards[y_now, x_now].Piece.Moves)
                            {
                                //cek apakah move yang akan dilakukan bakal out of bounds dari array boards
                                if (y_now + move.AddY >= 0 && y_now + move.AddY < 8 &&
                                    x_now + move.AddX >= 0 && x_now + move.AddX < 4)
                                {
                                    //move yang valid diubah jadi hijau
                                    boards[y_now + move.AddY, x_now + move.AddX].ShownColor = green;

                                }

                            }

                            //lakukan pengecekan jika menabrak piece musuh atau teman
                            //lakukan pengecekan jika valid move ternyata terhalang piece lainnya
                            foreach (Move move in boards[y_now, x_now].Piece.Moves)
                            {
                                //cek apakah move yang akan dilakukan bakal out of bounds dari array boards
                                if (y_now + move.AddY >= 0 && y_now + move.AddY < 8 &&
                                    x_now + move.AddX >= 0 && x_now + move.AddX < 4)
                                {


                                    //kalo move dari piece saat ini menabrak piece lain, move jadi invalid

                                    int x = x_now;
                                    int y = y_now;
                                    if (boards[y_now + move.AddY, x_now + move.AddX].Piece.Color == boards[y_now, x_now].Piece.Color)
                                    {
                                        CheckBlock(move, x_now, y_now);


                                        boards[y_now + move.AddY, x_now + move.AddX].ShownColor = none;
                                    }
                                    //kalo move dari piece saat ini mengancam piece musuh, buat petak jadi warna merah
                                    if (boards[y_now + move.AddY, x_now + move.AddX].Piece.Color == opposite_color && boards[y_now + move.AddY, x_now + move.AddX].ShownColor == green)
                                    {

                                        CheckBlock(move, x_now, y_now);


                                        boards[y_now + move.AddY, x_now + move.AddX].ShownColor = red;


                                    }


                                }

                            }
                        }
                    }


                }
                //kalo click petak warna hijau atau merah, maka piece akan jalan
                else
                {
                    //switch play turn
                    if (white_turn)
                    {
                        white_turn = false;
                    }
                    else
                    {
                        white_turn = true;
                    }

                    

                    //cek apakah jalan biasa atau makan
                    //kalo makan, tambahkan ke list
                    if (boards[y_now, x_now].ShownColor == red)
                    {
                        if (piece_now.Color == white)
                        {
                            black_taken.Add(boards[y_now, x_now].Piece);
                        }
                        else if (piece_now.Color == black)
                        {
                            white_taken.Add(boards[y_now, x_now].Piece);
                        }

                        //draw ulang panel makan
                        DrawPanelTaken();
                    }

                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            //cari board yang ditekan sebelumnya
                            if (boards[i, j] == board_now)
                            {
                                

                                //reset piece pada boards
                                boards[i, j].Piece = new Piece();
                            }

                        }
                    }

                    //petak yang ditekan diisi dengan piece yang dipilih

                    

                    boards[y_now, x_now].Piece = piece_now;

                    //bersihkan board
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            boards[i, j].ShownColor = none;
                        }
                    }

                    //bersihkan piece saat ini
                    piece_now = new Piece();
                    //reset data board yang ditekan
                    board_now = new Board();
                }

                //gambar ulang papan, check apakah game sudah selesai
                DrawPanelGame();
                CheckGame();
            }
        

        }

    }
}
