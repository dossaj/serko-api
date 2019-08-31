using System;
using System.ComponentModel.Design;
using System.IO;
using System.Net.Mail;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Serko.Expense.Server.Formatters
{
    public class CustomTextReader : StreamReader
    {
        private int depth;
        private bool closed;
        private int openTagPos;
        private int closeTagPos;
        private readonly Type type;
        private readonly string openTag;
        private readonly string closeTag;

        private readonly StringBuilder builder;
 
        public CustomTextReader(Stream stream, Type type) 
            : base(stream)
        {
            this.type = type;
            closed = true;
            openTag = $"<{type.Name}>";
            closeTag = $"</{type.Name}>";

            builder = new StringBuilder();
        }

        public override int Read()
        {
            if (depth == 1 && closed)
            {
                Skip();
            }

            var c = ReadNext();
            var n = PeekNext();
            builder.Append((char)c);

            if (c == 60 && n != 47)
            {
                depth++;
                closed = false;
            }

            if (c == 60 && n == 47)
            {
                depth--;
                closed = false;
            }

            if (c == 62)
            {
                closed = true;
            }

            return c;
        }

        public override int Read(char[] buffer, int index, int count)
        {
            for (var i = index; i < count; ++i)
            {
                var c = Read();
                if (c == -1)
                {
                    return i - index;
                }

                buffer[i] = (char)c;
            }
            return count;
        }
        
        protected virtual void Skip()
        {
            while (Peek() != 60 && Peek() != -1)
            {
                base.Read();
            }
        }

        private int PeekNext()
        {
            var next = openTagPos + 1;
            if (next < openTag.Length)
            {
                return openTag[next];
            }

            var c = Peek();
            if (c != -1)
            {
                return c;
            }

            next = closeTagPos + 1;
            if (next < closeTag.Length)
            {
                return closeTag[next];
            }
            return -1;
        }

        private int ReadNext()
        {
            if (openTagPos < openTag.Length)
            {
                return openTag[openTagPos++];
            }

            var c = base.Read();
            if (c != -1)
            {
                return c;
            }

            if (closeTagPos < closeTag.Length)
            {
                return closeTag[closeTagPos++];
            }
            return -1;
        }
    }
}