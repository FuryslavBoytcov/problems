
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
    .heatMap tr:nth-child(5) { background: gold; color: black;  }
    .heatMap tr:nth-child(2) { background: lightgreen; color: black;  }
    .heatMap tr:nth-child(3) { background: green; color: black;  }

    .heatMap tr:nth-child(9) { background: gold; color: black;  }
    .heatMap tr:nth-child(10) { background: green; color: black;  }
    .heatMap tr:nth-child(11) { background: lightgreen; color: black;  }

    .heatMap tr:nth-child(16) { background: gold; color: black;  }
    .heatMap tr:nth-child(17) { background: green; color: black;  }
    .heatMap tr:nth-child(19) { background: lightgreen; color: black;  }
</style>

<div class="heatMap">

|                            Method | SourceLength |               Mean |            Error |            StdDev |             Median |        Gen0 |       Gen1 |       Gen2 |   Allocated |
|---------------------------------- |------------- |-------------------:|-----------------:|------------------:|-------------------:|------------:|-----------:|-----------:|------------:|
|                            Oxffaa |         1024 |           754.3 ns |          1.98 ns |           1.75 ns |           754.6 ns |      0.5236 |     0.0019 |          - |      3288 B |
|                           Bzhemba |         1024 |         1,498.7 ns |          1.72 ns |           1.53 ns |         1,498.7 ns |      0.5188 |     0.0019 |          - |      3264 B |
|                           Valeriy |         1024 |         1,590.9 ns |          3.70 ns |           3.46 ns |         1,590.8 ns |      0.5131 |     0.0019 |          - |      3224 B |
|                     Pres_Advanced |         1024 |         1,683.5 ns |          4.68 ns |           4.15 ns |         1,682.3 ns |      0.1049 |          - |          - |       664 B |
|                          DyatlovA |         1024 |         1,690.4 ns |          3.34 ns |           2.78 ns |         1,690.2 ns |      0.1049 |          - |          - |       664 B |
| Pres_SimplestImprovedTimeAndSpace |         1024 |         3,008.1 ns |          2.53 ns |           2.37 ns |         3,007.5 ns |      0.1030 |          - |          - |       664 B |
|                              Rafa |         1024 |        33,574.8 ns |         41.11 ns |          32.10 ns |        33,579.1 ns |     25.0854 |     0.1831 |          - |    157640 B |
|                     Pres_Advanced |      1048576 |     1,857,273.1 ns |      2,929.76 ns |       2,597.15 ns |     1,856,991.2 ns |    123.0469 |    93.7500 |    87.8906 |    538683 B |
|                          DyatlovA |      1048576 |     1,861,387.9 ns |      1,632.15 ns |       1,362.92 ns |     1,861,317.0 ns |    123.0469 |    93.7500 |    87.8906 |    538683 B |
|                           Bzhemba |      1048576 |     2,565,067.5 ns |      5,428.85 ns |       4,533.34 ns |     2,564,852.7 ns |    542.9688 |   316.4063 |   125.0000 |   3245771 B |
|                           Valeriy |      1048576 |     2,650,074.6 ns |      9,828.45 ns |       9,193.53 ns |     2,646,935.1 ns |    542.9688 |   312.5000 |   125.0000 |   3245058 B |
|                            Oxffaa |      1048576 |     2,748,855.9 ns |      8,927.27 ns |       7,913.79 ns |     2,750,465.9 ns |    558.5938 |   406.2500 |   136.7188 |   3190979 B |
| Pres_SimplestImprovedTimeAndSpace |      1048576 |     2,950,857.0 ns |      2,894.26 ns |       2,565.69 ns |     2,950,498.2 ns |    121.0938 |    89.8438 |    85.9375 |    538683 B |
|                              Rafa |      1048576 |   200,268,064.8 ns |  1,296,122.77 ns |   1,212,394.04 ns |   200,188,875.0 ns |  21333.3333 |  8666.6667 |  3000.0000 | 121043464 B |
|                     Pres_Advanced |      5242880 |     9,482,060.6 ns |     12,486.13 ns |      10,426.49 ns |     9,482,046.9 ns |    156.2500 |   140.6250 |   125.0000 |   2327376 B |
|                          DyatlovA |      5242880 |     9,508,317.9 ns |     22,104.74 ns |      19,595.28 ns |     9,500,662.1 ns |    156.2500 |   125.0000 |   125.0000 |   2327376 B |
|                           Valeriy |      5242880 |    14,469,264.9 ns |     41,701.39 ns |      39,007.51 ns |    14,471,196.6 ns |   2312.5000 |   859.3750 |   375.0000 |  15763492 B |
| Pres_SimplestImprovedTimeAndSpace |      5242880 |    14,997,765.8 ns |     18,221.10 ns |      16,152.53 ns |    14,993,742.8 ns |    187.5000 |   156.2500 |   156.2500 |   2327396 B |
|                           Bzhemba |      5242880 |    15,129,661.7 ns |    114,724.77 ns |     107,313.62 ns |    15,169,146.5 ns |   2359.3750 |   906.2500 |   359.3750 |  15766210 B |
|                            Oxffaa |      5242880 |    15,846,805.9 ns |    200,801.74 ns |     187,830.07 ns |    15,861,957.7 ns |   2312.5000 |   968.7500 |   343.7500 |  16048929 B |
|                              Rafa |      5242880 | 1,567,408,391.2 ns | 40,596,538.02 ns | 119,699,808.30 ns | 1,601,270,208.0 ns | 108000.0000 | 45000.0000 | 13000.0000 | 598488440 B |
 
</div>