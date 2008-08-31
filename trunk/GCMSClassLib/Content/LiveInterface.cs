
	using System;
	using System.Reflection;

	namespace GCMSClassLib.Content
	{

		/// <summary>
		/// The real plug-in interface we use to communicate across app-domains
		/// </summary>
		public interface ILiveInterface
		{
			/// <summary> just one sample method to be called across app-domain</summary>
			/// <param name="inpString"> any string the method will modify </param>
			/// <returns> returns the modified string passed in as 'inpString' </returns>
			string ModifyString( string inpString );
		}



		/// <summary>
		/// Factory class to create objects exposing ILiveInterface
		/// </summary>
		public class LiveInterfaceFactory : MarshalByRefObject
		{
			private const BindingFlags bfi = BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance;

			public LiveInterfaceFactory() {}

			/// <summary> Factory method to create an instance of the type whose name is specified,
			/// using the named assembly file and the constructor that best matches the specified parameters. </summary>
			/// <param name="assemblyFile"> The name of a file that contains an assembly where the type named typeName is sought. </param>
			/// <param name="typeName"> The name of the preferred type. </param>
			/// <param name="constructArgs"> An array of arguments that match in number, order, and type the parameters of the constructor to invoke, or null for default constructor. </param>
			/// <returns> The return value is the created object represented as ILiveInterface. </returns>
			public ILiveInterface Create( string assemblyFile, string typeName, object[] constructArgs )
			{
				return (ILiveInterface) Activator.CreateInstanceFrom(
					assemblyFile, typeName, false, bfi, null, constructArgs,
					null, null, null ).Unwrap();
			}

		}


	}
