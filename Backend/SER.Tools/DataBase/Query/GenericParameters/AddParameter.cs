namespace SER.Tools.DataBase.Query.GenericParameters;

public delegate IParameter AddParameter<T>(Query query, T value, string name);