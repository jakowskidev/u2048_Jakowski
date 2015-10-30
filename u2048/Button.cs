/************************
 * @author Łukasz Jakowski
 * @since  08.04.2014 15:03
 * 
 ************************/

using System;
using System.Drawing;

namespace u2048
{
    class Button
    {
        private int iXPos, iYPos;
        private int iWidth, iHeight;
        private int imgID;

        private Boolean clickable;

        public Button(int iXPos, int iYPos, int iWidth, int iHeight, int imgID, Boolean clickable)
        {
            this.iXPos = iXPos;
            this.iYPos = iYPos;
            this.iWidth = iWidth;
            this.iHeight = iHeight;
            this.imgID = imgID;
            this.clickable = clickable;
        }

        /* ******************************************** */

        public void Draw(Graphics g, Bitmap oB)
        {
            g.DrawImage(oB, new Point(iXPos, iYPos));
        }

        /* ******************************************** */

        public int getXpos()
        {
            return iXPos;
        }

        public int getYPos()
        {
            return iYPos;
        }

        public int getWidth()
        {
            return iWidth;
        }

        public int getHeight()
        {
            return iHeight;
        }

        public int getIMGID()
        {
            return imgID;
        }

        public Boolean getClickable()
        {
            return clickable;
        }
    }
}
