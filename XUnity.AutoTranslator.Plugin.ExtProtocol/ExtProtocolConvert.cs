﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace XUnity.AutoTranslator.Plugin.ExtProtocol
{
   public static class ExtProtocolConvert
   {
      private static readonly Dictionary<string, Type> IdToType = new Dictionary<string, Type>();
      private static readonly Dictionary<Type, string> TypeToId = new Dictionary<Type, string>();

      static ExtProtocolConvert()
      {
         Register( TranslationRequest.Type, typeof( TranslationRequest ) );
         Register( TranslationResponse.Type, typeof( TranslationResponse ) );
         Register( TranslationError.Type, typeof( TranslationError ) );
      }

      internal static void Register( string id, Type type )
      {
         IdToType[ id ] = type;
         TypeToId[ type ] = id;
      }

      public static string Encode( ProtocolMessage message )
      {
         var writer = new StringWriter();
         var id = TypeToId[ message.GetType() ];
         writer.WriteLine( id );
         message.Encode( writer );
         return Convert.ToBase64String( Encoding.UTF8.GetBytes( writer.ToString() ) );
      }

      public static ProtocolMessage Decode( string message )
      {
         var payload = Encoding.UTF8.GetString( Convert.FromBase64String( message ) );
         var reader = new StringReader( payload );
         var id = reader.ReadLine();
         var type = IdToType[ id ];
         var protocolMessage = (ProtocolMessage)Activator.CreateInstance( type );
         protocolMessage.Decode( reader );
         return protocolMessage;
      }
   }
}
