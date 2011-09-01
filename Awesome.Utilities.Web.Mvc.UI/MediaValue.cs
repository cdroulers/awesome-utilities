using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Web.Mvc.UI
{
    /// <summary>
    ///     The different kinds of media for a TypeMediaControl
    /// </summary>
    public enum MediaValue
    {
        /// <summary>
        ///     No media specified
        /// </summary>
        None,
        /// <summary>
        ///     All medias
        /// </summary>
        All,
        /// <summary>
        ///     Aural media
        /// </summary>
        Aural,
        /// <summary>
        ///     Braille media
        /// </summary>
        Braille,
        /// <summary>
        ///     Embossed media
        /// </summary>
        Embossed,
        /// <summary>
        /// 
        /// </summary>
        HandHeld,
        /// <summary>
        ///     print media
        /// </summary>
        Print,
        /// <summary>
        ///     projection media
        /// </summary>
        Projection,
        /// <summary>
        ///     screen media
        /// </summary>
        Screen,
        /// <summary>
        ///     TTY
        /// </summary>
        Tty,
        /// <summary>
        ///     TV
        /// </summary>
        TV,
    }
}
