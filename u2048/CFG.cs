/************************
 * @author Łukasz Jakowski
 * @since  08.04.2014 14:33
 * 
 ************************/

using System;

namespace u2048
{
    class CFG
    {
        private static CFG oCFG;

        private CFG() { }

        public static CFG getInstance()
        {
            if (oCFG == null)
            {
                oCFG = new CFG();
            }

            return oCFG;
        }

        /* ******************************************** */

        private const int iWidth = 396, iHeight = 640;

        private static long lTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

        /* ******************************************** */

        public int getWidth()
        {
            return iWidth;
        }

        public int getHeight()
        {
            return iHeight;
        }

        public long getCurrentTime()
        {
            return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

        public long getTime()
        {
            return lTime;
        }

        public void setTime(long lTime)
        {
            CFG.lTime = lTime;
        }
    }
}
