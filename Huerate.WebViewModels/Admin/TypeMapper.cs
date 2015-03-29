/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace Huerate.WebViewModels.Admin
{
    internal class TypeMapper<TGlobalOut> where TGlobalOut : class
    {
        private readonly List<Func<object, TGlobalOut>> _mappings = new List<Func<object, TGlobalOut>>();

        public void Map<TIn, TOut>() where TOut : TGlobalOut, new() where TIn : class
        {
            _mappings.Add(o =>
                              {
                                  if (o is TIn)
                                  {
                                      return new TOut();
                                  }

                                  return default(TOut);
                              });
        }

        public TGlobalOut Get<TIn>(TIn obj)
        {
            return _mappings.Select(m => m(obj)).First(o => o != null);
        }
    }
}