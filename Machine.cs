using System.Collections.Generic;
using System.Linq;

namespace FSMProject
{
    /// <summary>
    /// Главный класс конечного автомата.
    /// </summary>
    static class Machine
    {
        class State
        {
            public char input, output;
            public bool isRight;

            public State(char input, char output, bool flag)
            {
                this.input = input;
                this.output = output;
                isRight = flag;
            }
        }

        static List<State> statesList = new List<State>(0);
        
        public static void Reset()
        {
            statesList = new List<State>(0); //Указываем на новый, а старый по идее должен удалиться сборщиком мусора
        }

        public static bool RightOutputExistsForInput(char input)
        {
            return GetRightStateForInput(input) != null;
        }

        public static bool IsOutputWrongForInput(char input, char output)
        {
            List<State> stList = GetStatesForInput(input);
            if (stList == null)
                return false;

            foreach (State st in stList)
            {
                if (st.input == input && st.output == output && !st.isRight)
                    return true;
            }

            return false;
        }

        public static char GetRightOutputForInput(char input)
        {
            State st = GetRightStateForInput(input);

            return st != null ? st.output : '\0';
        }

        public static void SetRightOutputForInput(char input, char output)
        {
            statesList.Add(new State(input, output, true));
        }

        public static void SetWrongOutputForInput(char input, char output)
        {
            statesList.Add(new State(input, output, false));
        }

        static List<State> GetStatesForInput(char input)
        {
            List<State> retList = new List<State>(0);

            foreach (State st in statesList)
            {
                if (st.input == input)
                    retList.Add(st);
            }
            return retList.Any() ? retList : null;
        }

        static State GetRightStateForInput(char input)
        {
            List<State> stList = GetStatesForInput(input);
            if (stList == null)
                return null;

            foreach (State st in stList)
            {
                if (st.input == input && st.isRight)
                    return st;
            }

            return null;
        }
    }
}
