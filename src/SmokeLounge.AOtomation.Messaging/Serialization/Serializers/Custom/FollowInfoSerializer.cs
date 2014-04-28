using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmokeLounge.AOtomation.Messaging.Serialization.Serializers.Custom
{
    using System;
    using System.Linq.Expressions;

    using SmokeLounge.AOtomation.Messaging.GameData;
    using SmokeLounge.AOtomation.Messaging.Messages.N3Messages;

    public class FollowInfoSerializer : ISerializer
    {       
        #region Fields

        private readonly Type type;

        #endregion

        #region Constructors and Destructors

        public FollowInfoSerializer()
        {
            this.type = typeof(FollowInfo);
        }

        #endregion

        #region Public Properties

        public Type Type
        {
            get
            {
                return this.type;
            }
        }

        #endregion

        public object Deserialize(
            StreamReader streamReader,
            SerializationContext serializationContext,
            PropertyMetaData propertyMetaData = null)
        {
            byte infoType = streamReader.ReadByte();
            if (infoType == 1)
            {
                var followCoordinateInfo = new FollowCoordinateInfo();
                followCoordinateInfo.FollowInfoType = 1;
                followCoordinateInfo.MoveMode = streamReader.ReadByte();
                followCoordinateInfo.CoordinateCount = streamReader.ReadByte();
                followCoordinateInfo.CurrentCoordinates.X = streamReader.ReadSingle();
                followCoordinateInfo.CurrentCoordinates.Y = streamReader.ReadSingle();
                followCoordinateInfo.CurrentCoordinates.Z = streamReader.ReadSingle();
                followCoordinateInfo.EndCoordinates.X = streamReader.ReadSingle();
                followCoordinateInfo.EndCoordinates.Y = streamReader.ReadSingle();
                followCoordinateInfo.EndCoordinates.Z = streamReader.ReadSingle();
                return followCoordinateInfo;
            }
            if (infoType == 2)
            {
                var followTargetInfo = new FollowTargetInfo();
                followTargetInfo.FollowInfoType = 2;
                followTargetInfo.DataLength = streamReader.ReadByte();
                IdentityType itype = (IdentityType)streamReader.ReadInt32();

                followTargetInfo.Target = new Identity() { Type = itype, Instance = streamReader.ReadInt32() };
                followTargetInfo.Dummy = streamReader.ReadByte();
                followTargetInfo.Dummy1 = streamReader.ReadInt32();
                followTargetInfo.Dummy2 = streamReader.ReadInt32();
                followTargetInfo.Dummy3 = streamReader.ReadInt32();
                followTargetInfo.Dummy4 = streamReader.ReadInt32();
                return followTargetInfo;
            }

            streamReader.Position = streamReader.Position - 1;
            return null;
        }

        public void Serialize(
            StreamWriter streamWriter,
            SerializationContext serializationContext,
            object value,
            PropertyMetaData propertyMetaData = null)
        {            if (value == null)
            {
                return;
            }

            var ftinfo = value as FollowTargetInfo;
            if (ftinfo != null)
            {
                streamWriter.WriteByte(ftinfo.FollowInfoType);
                streamWriter.WriteByte(ftinfo.DataLength);
                streamWriter.WriteInt32((int)ftinfo.Target.Type);
                streamWriter.WriteInt32(ftinfo.Target.Instance);
                streamWriter.WriteByte(ftinfo.Dummy);
                streamWriter.WriteInt32(ftinfo.Dummy1);
                streamWriter.WriteInt32(ftinfo.Dummy2);
                streamWriter.WriteInt32(ftinfo.Dummy3);
                streamWriter.WriteInt32(ftinfo.Dummy4);
            }
            var fcinfo = value as FollowCoordinateInfo;
            if (fcinfo != null)
            {
                streamWriter.WriteByte(fcinfo.FollowInfoType);
                streamWriter.WriteByte(fcinfo.MoveMode);
                streamWriter.WriteByte(fcinfo.CoordinateCount);
                streamWriter.WriteSingle(fcinfo.CurrentCoordinates.X);
                streamWriter.WriteSingle(fcinfo.CurrentCoordinates.Y);
                streamWriter.WriteSingle(fcinfo.CurrentCoordinates.Z);
                streamWriter.WriteSingle(fcinfo.EndCoordinates.X);
                streamWriter.WriteSingle(fcinfo.EndCoordinates.Y);
                streamWriter.WriteSingle(fcinfo.EndCoordinates.Z);
            }
        }

        public Expression DeserializerExpression(
            ParameterExpression streamReaderExpression, 
            ParameterExpression serializationContextExpression, 
            Expression assignmentTargetExpression, 
            PropertyMetaData propertyMetaData)
        {
            var deserializerMethodInfo =
                ReflectionHelper
                    .GetMethodInfo
                    <FollowInfoSerializer, Func<StreamReader, SerializationContext, PropertyMetaData, object>>(
                        o => o.Deserialize);
            var serializerExp = Expression.New(this.GetType());
            var callExp = Expression.Call(
                serializerExp, 
                deserializerMethodInfo, 
                new Expression[]
                    {
                        streamReaderExpression, serializationContextExpression, 
                        Expression.Constant(propertyMetaData, typeof(PropertyMetaData))
                    });

            var assignmentExp = Expression.Assign(
                assignmentTargetExpression, Expression.TypeAs(callExp, assignmentTargetExpression.Type));
            return assignmentExp;
        }
                public Expression SerializerExpression(
            ParameterExpression streamWriterExpression, 
            ParameterExpression serializationContextExpression, 
            Expression valueExpression, 
            PropertyMetaData propertyMetaData)
        {
            var serializerMethodInfo =
                ReflectionHelper
                    .GetMethodInfo
                    <FollowInfoSerializer, Action<StreamWriter, SerializationContext, object, PropertyMetaData>>(o => o.Serialize);
            var serializerExp = Expression.New(this.GetType());
            var callExp = Expression.Call(
                serializerExp, 
                serializerMethodInfo, 
                new[]
                    {
                        streamWriterExpression, serializationContextExpression, valueExpression, 
                        Expression.Constant(propertyMetaData, typeof(PropertyMetaData))
                    });
            return callExp;
        }
    }
}
