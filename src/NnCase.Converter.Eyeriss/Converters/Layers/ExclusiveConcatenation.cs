﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NnCase.Converter.Converters;
using NnCase.Converter.Eyeriss.Converters.Stages.AssignBuffer;
using NnCase.Converter.Model;
using NnCase.Converter.Model.Layers;

namespace NnCase.Converter.Eyeriss.Converters.Layers
{
    [LayerConverter(typeof(ExclusiveConcatenation), EyerissLayerType.Invalid)]
    public class ExclusiveConcatenationConverter
    {
        public void AllocateInputMemory(ExclusiveConcatenation layer, OutputConnector input, AssignBufferContext context)
        {
            var totalAlloc = context.GetOrAllocateMainMemory(layer.Output);
            uint offset = 0;

            foreach (var node in layer.Inputs.Select(x => x.Connection.From))
            {
                if (context.MainMemoryMap.ContainsKey(node))
                    return;
                uint size = (uint)node.Dimensions.GetSize() * 4;
                context.MainMemoryMap.Add(node, new MemoryAllocation(totalAlloc.Node, offset, size));
                offset += size;
            }
        }
    }
}
