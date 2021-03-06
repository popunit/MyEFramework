﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace eCommerce.Web.Framework.Theme
{
    /// <summary>
    /// Represent theme content in associated configuration file
    /// </summary>
    public class ThemeConfig
    {
        public ThemeConfig(string themeName, string path, XmlDocument doc)
        {
            this.ThemeName = themeName;
            this.Path = path;
            var node = doc.SelectSingleNode("theme");
            if (null != node)
            {
                this.Node = node;
                var attribute = node.Attributes["title"];
                this.Title = null == attribute ? string.Empty : attribute.Value;
                attribute = node.Attributes["supportRTL"];
                SupportRtl = attribute == null ? false : bool.Parse(attribute.Value);
                attribute = node.Attributes["isForMobile"];
                IsForMobile = attribute == null ? false : bool.Parse(attribute.Value);
                attribute = node.Attributes["previewImageUrl"];
                PreviewImageUrl = attribute == null ? string.Empty : attribute.Value;
                attribute = node.Attributes["previewText"];
                PreviewText = attribute == null ? string.Empty : attribute.Value;
            }
        }

        public string ThemeName { get; protected set; }
        public string Path { get; protected set; }

        public XmlNode Node { get; protected set; }
        public string Title { get; protected set; }
        /// <summary>
        /// Indicate if target theme (css) supports RTL mode
        /// </summary>
        public bool SupportRtl { get; protected set; }
        /// <summary>
        /// Indicate if target theme is used for mobile mode
        /// </summary>
        public bool IsForMobile { get; protected set; }
        /// <summary>
        /// Preview image url
        /// </summary>
        /// <remarks>you can provide an image to show how does the theme look like</remarks>
        public string PreviewImageUrl { get; protected set; }
        /// <summary>
        /// Description for the preview
        /// </summary>
        public string PreviewText { get; protected set; }
    }
}
