using ChessApp.Chesspieces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChessApp.DataBaseAccess;

public class ChesspieceJsonConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(IChesspiece);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JObject item = JObject.Load(reader);
            // Use item data to determine the concrete type
            // For example, assuming you have a property "PieceType" in your JSON
            ChesspieceName pieceType = item["Name"].ToObject<ChesspieceName>();
            switch (pieceType)
            {
                case ChesspieceName.Bishop:
                    return item.ToObject<Bishop>(); 
                case ChesspieceName.King:
                    return item.ToObject<King>(); 
                case ChesspieceName.Knight:
                    return item.ToObject<Knight>(); 
                case ChesspieceName.None:
                    return item.ToObject<NoPiece>(); 
                case ChesspieceName.Pawn:
                    return item.ToObject<Pawn>(); 
                case ChesspieceName.Queen:
                    return item.ToObject<Queen>(); 
                case ChesspieceName.Rook:
                    return item.ToObject<Rook>(); 
                default:
                    throw new JsonSerializationException("Unknown piece type");
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException(); // Implement if you need serialization
        }
    }