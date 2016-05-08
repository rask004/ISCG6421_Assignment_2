using System;

namespace AddStrip
{
	public enum Operator {plus,minus,times,divide,subtotal,total, error};
	//data type that holds literal code for allowable operators

	/// <summary> class CalcLine
	/// class to represent individual lines in an adding strip calculation
	/// </summary>
	public class CalcLine
	{
		private Operator op;
		private double number;

		/// <summary> constructor CalcLine
		/// create a CalcLine object from an Operator value and a double value
		/// </summary>
		public CalcLine(Operator OP, double num)
		{
			op = OP;
			number = num;
		}

		/// <summary> constructor CalcLine
		/// create a CalcLine object from an Operator value
		/// </summary>
		public CalcLine(Operator OP)
		{
			op = OP;
			number = 0;
		}
	
		/// <summary> constructor CalcLine
		/// create a CalcLine object from a string value 
		/// </summary>
		public CalcLine(string calcLine)
			//assume format 123, +123, = (total) or # (subtotal)
		{
			char ch = calcLine[0];
			if (ch == '=')
			{
				op = Operator.total;
				number = 0;
			}
			else if (ch == '#')
			{
				op = Operator.subtotal;
				number = 0;
			}
			else if ((ch >= (char)48) && (ch <= (char)57)) //numeric
			{
				op = Operator.plus;
				number = Convert.ToDouble(calcLine);
			}
			else
			{
				op = CharToOp(ch);
				string snum = calcLine.Substring(1);
				number = Convert.ToDouble(snum);
			}
		}

		public Operator Op //public property that gives access to the private op data field
		{
			set {op = value;}
			get {return op;}
		}
		
		/// <summary> method ToString
		/// return a string giving the operator and number of CalcLine object
		/// or just the operator if the operation is for a total or subtotal
		/// </summary>
		public override string ToString()
		{
			if (op == Operator.total)
			{
				return "=";
			}
			else if (op == Operator.subtotal)
			{
				return "#";
			}
			else
			{
				return (OpToString(op) + " " + Convert.ToString(number));
			}
		}

		/// <summary> method NextResult
		/// return a double giving the result of applying the CalcLine object
		/// to the value passed in the double parameter ResultSoFar
		/// 
		/// Used to get the cumulative result of a set of CalcLine objects
		/// </summary>
		public double NextResult(double ResultSoFar)
		{
			if (op == Operator.plus) return ResultSoFar + number;
			else if(op == Operator.minus) return ResultSoFar - number;
			else if(op == Operator.times) return ResultSoFar * number;
			else if(op == Operator.divide) return ResultSoFar / number;
			else if(op == Operator.subtotal) return ResultSoFar;
			else if(op == Operator.total) return ResultSoFar;
			else return -1;
		}
			
		/// <summary> method OpToString
		/// return a string giving the string version of the operator held
		/// by the CalcLine object
		/// </summary>
		public static string OpToString(Operator OP)
		{
			if (OP == Operator.plus) return "+";
			else if(OP == Operator.minus) return "-";
			else if(OP == Operator.times) return "*";
			else if(OP == Operator.divide) return "/";
			else if(OP == Operator.subtotal) return "#";
			else if(OP == Operator.total) return "=";
			else return "Error";
		}

		/// <summary> method CharToOp
		/// return the Operator value matching a given char value
		/// 
		/// Can be used to check whether a char converts to a valid Operator
		/// </summary>
		public static Operator CharToOp(char strOp)
		{
			if (strOp == '+') return Operator.plus;
			else if (strOp == '-') return Operator.minus;
			else if (strOp == '*') return Operator.times;
			else if (strOp == '/') return Operator.divide;
			else if (strOp == '#') return Operator.subtotal;
			else if (strOp == '=') return Operator.total;
			else return Operator.error;
		}

	}
}
