using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishBotMAUI.Services.Utility
{
    public static class TypeUtility
    {
        public static IEnumerable<Type> AllSubTypes (this Type type, bool allowAbstract = false)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var subType in assembly.GetTypes())
                {
                    if (type.IsAssignableFrom(subType))
                    {
                        if (!allowAbstract && subType.IsAbstract)
                        {
                            continue;
                        }

                        yield return subType;
                    }
                }
            }
            
        }
    }
}
