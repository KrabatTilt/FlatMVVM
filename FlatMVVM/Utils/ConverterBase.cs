﻿using System;
using System.Windows.Markup;

namespace TT.FlatMVVM.Wpf.Utils
{
    /// <inheritdoc />
    /// <summary>
    /// Use ValueConverters as markup extension.
    /// </summary>
    public abstract class ConverterBase : MarkupExtension
    {

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

    }
}
