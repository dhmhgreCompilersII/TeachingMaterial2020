using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachingMaterial {
    public abstract class CNodeType<T> {
        private T m_nodeType;

        protected CNodeType(T nodeType) {
            m_nodeType = nodeType;
        }

        /// <summary>
        /// Returns Default Type Value
        /// </summary>
        /// <returns></returns>
        public abstract T Default();
        /// <summary>
        /// Returns Not Applicable Type Value
        /// </summary>
        /// <returns></returns>
        public abstract T NA();

        /// <summary>
        /// Maps an integer to a type value
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public abstract T Map(int type);

        /// <summary>
        /// Maps a type value to an integer
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public abstract int Map(T type);
    }
}
