/************************
 * @author Łukasz Jakowski
 * @since  08.04.2014 14:47
 * 
 ************************/

using System;
using System.Collections.Generic;
using System.Drawing;

namespace u2048
{
    class Game
    {
        private enum GameState
        {
            eGame,
            eAbout,
        };

        private GameState currentGameState = GameState.eGame;

        private int[][] iBoard;

        private int iScore = 0, iBest = 0;

        private List<Button> oButton = new List<Button>();
        private List<Bitmap> oBitmap = new List<Bitmap>();

        private Font fFontS2 = new Font("Clear Sans", 10, FontStyle.Bold);
        private Font fFontS = new Font("Clear Sans", 12, FontStyle.Bold);
        private Font fFont = new Font("Clear Sans", 22, FontStyle.Bold);
        private SizeF stringSize = new SizeF();

        private int addNum = 2;

        private Random oR = new Random();

        private Boolean gameOver = false;
        private Rectangle rRect;

        private int iNewX, iNewY;

        public Boolean kTOP, kRIGHT, kBOTTOM, kLEFT;

        public Boolean bRender = true;

        public enum Direction
        {
            eTOP,
            eRIGHT,
            eBOTTOM,
            eLEFT,
        };

        public Game()
        {
            this.iBoard = new int[4][];
            for (int i = 0; i < 4; i++)
            {
                iBoard[i] = new int[4];
            }

            oBitmap.Add(new Bitmap(@"images/1.png"));
            oBitmap.Add(new Bitmap(@"images/2.png"));
            oBitmap.Add(new Bitmap(@"images/3.png"));
            oBitmap.Add(new Bitmap(@"images/4.png"));
            oBitmap.Add(new Bitmap(@"images/5.png"));
            oBitmap.Add(new Bitmap(@"images/6.png"));
            oBitmap.Add(new Bitmap(@"images/7.png"));
            oBitmap.Add(new Bitmap(@"images/8.png"));
            oBitmap.Add(new Bitmap(@"images/9.png"));
            oBitmap.Add(new Bitmap(@"images/k0.png"));
            oBitmap.Add(new Bitmap(@"images/10.png"));
            oBitmap.Add(new Bitmap(@"images/11.png"));
            oBitmap.Add(new Bitmap(@"images/12.png"));
            oBitmap.Add(new Bitmap(@"images/13.png"));
            oBitmap.Add(new Bitmap(@"images/14.png"));
            oBitmap.Add(new Bitmap(@"images/15.png"));
            oBitmap.Add(new Bitmap(@"images/16.png"));
            oBitmap.Add(new Bitmap(@"images/17.png"));
            oBitmap.Add(new Bitmap(@"images/18.png"));

            oButton.Add(new Button(18, 18, 125, 130, 0, true));  // -- LEFT BIG
 
            oButton.Add(new Button(161, 18, 100, 66, 1, false)); // -- SCORE
            oButton.Add(new Button(279, 18, 100, 66, 1, false)); // -- BEST

            oButton.Add(new Button(161, 96, 100, 38, 2, true));  // -- NEW GAME
            oButton.Add(new Button(279, 96, 100, 38, 2, true));  // -- ABOUT

            oButton.Add(new Button(64, 103, 32, 32, 9, true)); // -- S
            oButton.Add(new Button(64, 30, 32, 32, 9, true));  // -- W
            oButton.Add(new Button(30, 67, 32, 32, 9, true));  // -- A
            oButton.Add(new Button(97, 67, 32, 32, 9, true));  // -- D

            rRect = new Rectangle(0, 0, 416, 640);
        }

        /* ******************************************** */

        public void Update()
        {
            while (!gameOver && addNum > 0)
            {
                int nX = oR.Next(0, 4), nY = oR.Next(0, 4);

                if (iBoard[nX][nY] == 0)
                {
                    iBoard[nX][nY] = oR.Next(0, 20) == 0 ? oR.Next(0, 15) == 0 ? 8 : 4 : 2;
                    iNewX = nX;
                    iNewY = nY;
                    --addNum;
                }
            }
        }

        public void Draw(Graphics g)
        {
            switch (currentGameState)
            {
                case GameState.eGame:
                    DrawGame(g);
                    if (gameOver)
                    {
                        GameOverDraw(g);
                    }

                    bRender = false;
                    break;
                case GameState.eAbout:
                    DrawGame(g);
                    DrawAbout(g);

                    bRender = false;
                    break;
            }
        }

        public void DrawGame(Graphics g)
        {
            for (int i = 0; i < oButton.Count; i++)
            {
                oButton[i].Draw(g, oBitmap[oButton[i].getIMGID()]);
            }

            DrawTextCenterXWS(g, "SCORE", fFontS2, new SolidBrush(Color.FromArgb(64, 10, 10, 10)), new SolidBrush(Color.FromArgb(235, 221, 208)), 211, 32);
            DrawTextCenterXWS(g, iScore.ToString(), fFontS, new SolidBrush(Color.FromArgb(64, 10, 10, 10)), new SolidBrush(Color.White), 211, 54);
            
            DrawTextCenterXWS(g, "BEST", fFontS2, new SolidBrush(Color.FromArgb(64, 10, 10, 10)), new SolidBrush(Color.FromArgb(235, 221, 208)), 329, 32);
            DrawTextCenterXWS(g, iBest.ToString(), fFontS, new SolidBrush(Color.FromArgb(64, 10, 10, 10)), new SolidBrush(Color.White), 329, 54);

            DrawTextCenterWS(g, "NEW GAME", fFontS2, new SolidBrush(Color.FromArgb(64, 10, 10, 10)), new SolidBrush(Color.FromArgb(255, 241, 224)), 211, 115);
            DrawTextCenterWS(g, "ABOUT", fFontS2, new SolidBrush(Color.FromArgb(64, 10, 10, 10)), new SolidBrush(Color.FromArgb(255, 241, 224)), 329, 115);
            
            DrawTextCenterWS(g, "W", fFontS2, new SolidBrush(Color.FromArgb(64, 10, 10, 10)), kTOP ? new SolidBrush(Color.FromArgb(247, 190, 48)) : new SolidBrush(Color.FromArgb(120, 110, 101)), 80, 46);
            DrawTextCenterWS(g, "S", fFontS2, new SolidBrush(Color.FromArgb(64, 10, 10, 10)), kBOTTOM ? new SolidBrush(Color.FromArgb(247, 190, 48)) : new SolidBrush(Color.FromArgb(120, 110, 101)), 80, 119);
            DrawTextCenterWS(g, "A", fFontS2, new SolidBrush(Color.FromArgb(64, 10, 10, 10)), kLEFT ? new SolidBrush(Color.FromArgb(247, 190, 48)) : new SolidBrush(Color.FromArgb(120, 110, 101)), 46, 83);
            DrawTextCenterWS(g, "D", fFontS2, new SolidBrush(Color.FromArgb(64, 10, 10, 10)), kRIGHT ? new SolidBrush(Color.FromArgb(247, 190, 48)) : new SolidBrush(Color.FromArgb(120, 110, 101)), 113, 83);

            g.DrawImage(oBitmap[3], new Point(18, 166));

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    g.DrawImage(oBitmap[i == iNewX && j == iNewY ? 18 : getBitmapID(iBoard[i][j])], new Point(30 + 87 * i, 178 + 87 * j));
                    if (iBoard[i][j] > 0)
                    {
                        DrawTextCenterWS(g, iBoard[i][j].ToString(), fFont, new SolidBrush(Color.FromArgb(64, 10, 10, 10)), (i == iNewX && j == iNewY ? new SolidBrush(Color.FromArgb(255, 255, 255)) : iBoard[i][j] < 8 ? new SolidBrush(Color.FromArgb(120, 110, 101)) : new SolidBrush(Color.FromArgb(249, 245, 235))), 68 + 87 * i, 217 + 87 * j);
                    }
                }
            }

            DrawTextCenterX(g, "www.LukaszJakowski.pl", fFontS2, new SolidBrush(Color.FromArgb(120, 110, 101)), 198, 552);
            DrawTextCenterX(g, "08-04-2014", fFontS2, new SolidBrush(Color.FromArgb(120, 110, 101)), 198, 566);
        }

        public void GameOverDraw(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.FromArgb(150, 251, 248, 239)), rRect);

            DrawTextCenterXWS(g, "GAME OVER", fFontS, new SolidBrush(Color.FromArgb(64, 10, 10, 10)), new SolidBrush(Color.FromArgb(120, 110, 101)), 198, 250);
            DrawTextCenterXWS(g, "SCORE: " + iScore.ToString(), fFontS, new SolidBrush(Color.FromArgb(64, 10, 10, 10)), new SolidBrush(Color.FromArgb(120, 110, 101)), 198, 282);
        }

        public void DrawAbout(Graphics g)
        {
            g.FillRectangle(new SolidBrush(Color.FromArgb(128, 255, 162, 89)), rRect);

            DrawTextCenterXWS(g, "2048 Puzzle Game - C#", fFontS, new SolidBrush(Color.FromArgb(255, 10, 10, 10)), new SolidBrush(Color.White), 198, 210);
            DrawTextCenterXWS(g, "Author: Łukasz Jakowski", fFontS, new SolidBrush(Color.FromArgb(255, 10, 10, 10)), new SolidBrush(Color.White), 198, 250);
            DrawTextCenterXWS(g, "Informatyka Inżynierska 2012-2016", fFontS, new SolidBrush(Color.FromArgb(255, 10, 10, 10)), new SolidBrush(Color.White), 198, 290);
            DrawTextCenterXWS(g, "Uniwersytet Śląski w Katowicach", fFontS, new SolidBrush(Color.FromArgb(255, 10, 10, 10)), new SolidBrush(Color.White), 198, 310);
            DrawTextCenterXWS(g, "www.LukaszJakowski.pl", fFontS, new SolidBrush(Color.FromArgb(255, 10, 10, 10)), new SolidBrush(Color.White), 198, 350);
            DrawTextCenterXWS(g, "08-04-2014", fFontS, new SolidBrush(Color.FromArgb(255, 10, 10, 10)), new SolidBrush(Color.White), 198, 370);
        }

        /* ******************************************** */

        public void DrawTextCenterX(Graphics g, String sText, Font nFont, SolidBrush nSolidBrush, int X, int Y)
        {
            stringSize = g.MeasureString(sText, nFont);
            g.DrawString(sText, nFont, nSolidBrush, new PointF(X - stringSize.Width / 2, Y));
        }

        public void DrawTextCenterXWS(Graphics g, String sText, Font nFont, SolidBrush nSolidBrush, SolidBrush nSolidBrush2, int X, int Y)
        {
            stringSize = g.MeasureString(sText, nFont);
            g.DrawString(sText, nFont, nSolidBrush, new PointF(X - stringSize.Width / 2 + 1, Y + 1));
            g.DrawString(sText, nFont, nSolidBrush2, new PointF(X - stringSize.Width / 2, Y));
        }

        public void DrawTextCenterWS(Graphics g, String sText, Font nFont, SolidBrush nSolidBrush, SolidBrush nSolidBrush2, int X, int Y)
        {
            stringSize = g.MeasureString(sText, nFont);
            g.DrawString(sText, nFont, nSolidBrush, new PointF(X - stringSize.Width / 2 + 1, Y - stringSize.Height / 2 + 1));
            g.DrawString(sText, nFont, nSolidBrush2, new PointF(X - stringSize.Width / 2, Y - stringSize.Height / 2));
        }

        /* ******************************************** */

        public void moveBoard(Direction nDirection)
        {
            Boolean bAdd = false;

            if (currentGameState == GameState.eAbout) currentGameState = GameState.eGame;

            switch (nDirection)
            {
                case Direction.eTOP:
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            for (int k = j + 1; k < 4; k++)
                            {
                                if (iBoard[i][k] == 0)
                                {
                                    continue;
                                }
                                else if (iBoard[i][k] == iBoard[i][j])
                                {
                                    iBoard[i][j] *= 2;
                                    iScore += iBoard[i][j];
                                    iBoard[i][k] = 0;
                                    bAdd = true;
                                    break;
                                }
                                else
                                {
                                    if (iBoard[i][j] == 0 && iBoard[i][k] != 0)
                                    {
                                        iBoard[i][j] = iBoard[i][k];
                                        iBoard[i][k] = 0;
                                        j--;
                                        bAdd = true;
                                        break;
                                    }
                                    else if(iBoard[i][j] != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case Direction.eRIGHT:
                    for (int j = 0; j < 4; j++)
                    {
                        for (int i = 3; i >= 0; i--)
                        {
                            for (int k = i - 1; k >= 0; k--)
                            {
                                if (iBoard[k][j] == 0)
                                {
                                    continue;
                                }
                                else if (iBoard[k][j] == iBoard[i][j])
                                {
                                    iBoard[i][j] *= 2;
                                    iScore += iBoard[i][j];
                                    iBoard[k][j] = 0;
                                    bAdd = true;
                                    break;
                                }
                                else
                                {
                                    if (iBoard[i][j] == 0 && iBoard[k][j] != 0)
                                    {
                                        iBoard[i][j] = iBoard[k][j];
                                        iBoard[k][j] = 0;
                                        i++;
                                        bAdd = true;
                                        break;
                                    }
                                    else if (iBoard[i][j] != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case Direction.eBOTTOM:
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 3; j >= 0; j--)
                        {
                            for (int k = j - 1; k >= 0; k--)
                            {
                                if (iBoard[i][k] == 0)
                                {
                                    continue;
                                }
                                else if (iBoard[i][k] == iBoard[i][j])
                                {
                                    iBoard[i][j] *= 2;
                                    iScore += iBoard[i][j];
                                    iBoard[i][k] = 0;
                                    bAdd = true;
                                    break;
                                }
                                else
                                {
                                    if (iBoard[i][j] == 0 && iBoard[i][k] != 0)
                                    {
                                        iBoard[i][j] = iBoard[i][k];
                                        iBoard[i][k] = 0;
                                        j++;
                                        bAdd = true;
                                        break;
                                    }
                                    else if (iBoard[i][j] != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
                case Direction.eLEFT:
                    for (int j = 0; j < 4; j++)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            for (int k = i + 1; k < 4; k++)
                            {
                                if (iBoard[k][j] == 0)
                                {
                                    continue;
                                }
                                else if (iBoard[k][j] == iBoard[i][j])
                                {
                                    iBoard[i][j] *= 2;
                                    iScore += iBoard[i][j];
                                    iBoard[k][j] = 0;
                                    bAdd = true;
                                    break;
                                }
                                else
                                {
                                    if (iBoard[i][j] == 0 && iBoard[k][j] != 0)
                                    {
                                        iBoard[i][j] = iBoard[k][j];
                                        iBoard[k][j] = 0;
                                        i--;
                                        bAdd = true;
                                        break;
                                    }
                                    else if (iBoard[i][j] != 0)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;
            }

            if (iScore > iBest)
            {
                iBest = iScore;
            }

            if (bAdd)
            {
                ++addNum;
            }

            /* ----- GAME OVER ----- */

            checkGameOver();
            bRender = true;
        }

        public void checkGameOver()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i - 1 >= 0)
                    {
                        if (iBoard[i - 1][j] == iBoard[i][j])
                        {
                            return;
                        }
                    }

                    if (i + 1 < 4)
                    {
                        if (iBoard[i + 1][j] == iBoard[i][j])
                        {
                            return;
                        }
                    }

                    if (j - 1 >= 0)
                    {
                        if (iBoard[i][j - 1] == iBoard[i][j])
                        {
                            return;
                        }
                    }

                    if (j + 1 < 4)
                    {
                        if (iBoard[i][j + 1] == iBoard[i][j])
                        {
                            return;
                        }
                    }

                    if (iBoard[i][j] == 0)
                    {
                        return;
                    }
                }
            }

            gameOver = true;
        }

        /* ******************************************** */

        public int getBitmapID(int iNum)
        {
            switch (iNum)
            {
                case 0:
                    return 4;
                case 2:
                    return 5;
                case 4:
                    return 6;
                case 8:
                    return 7;
                case 16:
                    return 8;
                case 32:
                    return 10;
                case 64:
                    return 11;
                case 128:
                    return 12;
                case 256:
                    return 13;
                case 512:
                    return 14;
                case 1024:
                    return 15;
                case 2048:
                    return 16;
                case 4096: case 8192: case 16384:
                    return 17;
            }

            return 4;
        }

        public void checkButton(int nXPos, int nYPos)
        {
            for(int i = 0; i < oButton.Count; i++) {
                if(oButton[i].getClickable()) {
                    if (nXPos >= oButton[i].getXpos() && nXPos <= oButton[i].getXpos() + oButton[i].getWidth() && nYPos >= oButton[i].getYPos() && nYPos <= oButton[i].getYPos() + oButton[i].getHeight())
                    {
                        actionButton(i);
                    }
                }
            }
        }

        public void actionButton(int iButtonID)
        {
            switch (iButtonID)
            {
                case 0:
                    
                    break;
                case 3: // NEW GAME
                    resetGameData();
                    break;
                case 4:
                    if (currentGameState == GameState.eGame) currentGameState = GameState.eAbout;
                    else currentGameState = GameState.eGame;
                    break;
            }
            bRender = true;
        }

        /* ******************************************** */

        private void resetGameData()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    this.iBoard[i][j] = 0;
                }
            }

            this.addNum = 2;
            this.iScore = 0;
            this.gameOver = false;
            this.currentGameState = GameState.eGame;
            this.bRender = true;
        }
    }
}
