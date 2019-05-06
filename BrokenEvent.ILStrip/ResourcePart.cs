﻿using System;
using System.Collections;
using System.IO;

using Confuser.Renamer.BAML;

using Mono.Cecil;

namespace BrokenEvent.ILStrip
{
  internal class ResourcePart: IDisposable
  {
    private readonly Stream stream;
    private readonly string name;

    private readonly string typeName;
    private readonly BamlDocument baml;

    public ResourcePart(DictionaryEntry entry)
    {
      name = (string)entry.Key;
      stream = (Stream)entry.Value;

      if (Path.GetExtension(name) != ".baml")
        return;

      baml = BamlReader.ReadDocument(stream);

      foreach (BamlRecord record in baml)
      {
        if (record.Type == BamlRecordType.TypeInfo)
        {
          TypeInfoRecord typeInfo = (TypeInfoRecord)record;
          if (typeInfo.AssemblyId != 0)
            continue;

          typeName = typeInfo.TypeFullName;
          break;
        }

        if (record.Type == BamlRecordType.ElementStart)
          break;
      }
    }

    public string Name
    {
      get { return name; }
    }

    public string TypeName
    {
      get { return typeName; }
    }

    public BamlDocument Baml
    {
      get { return baml; }
    }

    public TypeDefinition TypeDef { get; set; }

    public void Dispose()
    {
      stream.Dispose();
    }
  }
}