<style>
    .heatMap {
        width: 100%;
        text-align: center;
    }
    .heatMap th {
        background: grey;
        word-wrap: break-word;
        text-align: center;
    }
    .heatMap tr:nth-child(1) { background: gold; color: black;  }
    .heatMap tr:nth-child(2) { background: gold; color: black;  }
    .heatMap tr:nth-child(3) { background: black; }

    .heatMap tr:nth-child(4) { background: green; color: black;  }
    .heatMap tr:nth-child(5) { background: lightgreen; color: black;  }
   
</style>

<div class="heatMap">

| Method   |        Mean |    Error |   StdDev | Allocated |
|----------|------------:|---------:|---------:|----------:|
| Oxffaa   |    94.89 us | 0.106 us | 0.100 us |         - |
| Dm       |   105.06 us | 0.098 us | 0.087 us |     536 B |
| Solution |   118.36 us | 0.019 us | 0.016 us |         - |
| Bzhemba  |   997.78 us | 2.006 us | 1.876 us |    2794 B |
| VMakeeva | 1,401.18 us | 1.089 us | 1.019 us |     402 B |

</div>