using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DebugLib
{
    public class KeyInterceptor
    {
        // This class must be defined inside of the KeyGrabber class to make use of the static functions defined inside of KeyGrabber.


        // The IMessageFilter interface allows an application to 
        // capture a message before it is dispatched to a control or form.
        public class KeyFilter : IMessageFilter
        {

            // PreFilterMessage:
            // Returns true if we want to filter the message and STOP it from being dispatched to the next filter or control.
            // Returns false if we want the message to continue to the next filter or control.
            // We can use this to perform work on the message before it gets dispatched to the next filter or control, which
            // is how we are using it here.

            // Receives a referece to Message m which contains a virtual key message.
            public bool PreFilterMessage(ref Message m)
            {


                /*
                    These are the message constants we will be watching for.
                */

                //  0×0100 is any key event that is not used with the ALT key.
                // If the message has this ID we use the Marshal class to grab us some unmanaged memory,
                // store the message in a pointer, and pass the message to the unmanaged TranslateMessage function. 
                // This function will translate the message into the true character that was pressed and post the 
                // 0×0102 (WM_CHAR) message to the form’s message queue.
                const int WM_KEYDOWN = 0x0100;


                //Stored in this message’s WParam is the transformed char we have been wanting all along! 
                // All that needs done is to convert the thing from an unsigned to a char with a simple cast
                // and we can use it!
                const int WCHAR_EVENT = 0x0102;

                if (m.Msg == WM_KEYDOWN)
                {
                    /*
                        The TranslateMessage function requires a pointer to be passed to it.
                        Since C# doesn't typically use pointers, we have to make use of the Marshal
                        class to store the Message into a pointer. We can then pass this pointer
                        over to the native function.
                    */

                    // Dynamically allocates enough memory to acomadate the size of m.
                    // since this function was passed the reference(address) of m,
                    // we can set point to point to the address of m.
                    IntPtr pointer = Marshal.AllocHGlobal(Marshal.SizeOf(m));

                    // Structure to pointer paramter list: 
                    // 1. A structure or managed object(such as our string) holding data needed to be Marshaled. This object
                    //    must be an instance of a formatted class.
                    // 2. A pointer to an unmanaged block of memory. This memory MUST BE ALLOCATED before this method is called.
                    // 3. Set to "true" to have the memory automatically deallocate the memory at the conclusion of this function.
                    Marshal.StructureToPtr(m, pointer, true);

                    // We can get virtual key messages stored in m and translate it to a char.
                    TranslateMessage(pointer);
                }
                else if (m.Msg == WCHAR_EVENT)
                {
                    //The WParam parameter contains the true character value
                    //we are after. Print this out to the screen and call the
                    //InboundCharEvent so any events hooked up to this will be
                    //notifed that there is a char ready to be processed.
                    char trueCharacter = (char)m.WParam;

                    // Debug to Console
                    //   Console.WriteLine(trueCharacter);

                    if (InboundCharEvent != null)
                        InboundCharEvent(trueCharacter);
                }

                //Returning false allows the message to continue to the next filter or control.
                return false;
            }

            // User32.dll is the library containing functions for Windows API such as WinForms and Dialogue Boxes.

            [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]


            // Extern signifies that this function is called externally. 
            // It basically indicates that we are using a function from an unmanage DLL and MUST be static. 
            // We can use this for many functions including the MessageBox function, which is the equivelant to
            // JFrame in Java.
            public static extern bool TranslateMessage(IntPtr message);
        }

        public static event Action<char> InboundCharEvent;
        static KeyInterceptor()
        {
            Application.AddMessageFilter(new KeyFilter());
        }
    }



}