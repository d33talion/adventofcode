var reader = new StreamReader(File.OpenRead("../../../../_input/day03.txt"));

var firstLine = await reader.ReadLineAsync();
if (string.IsNullOrEmpty(firstLine)) return;


var calculator = new RatesCalculator(firstLine.Length).ProcessLine(firstLine);

do
{
	var line = await reader.ReadLineAsync();
	if (string.IsNullOrEmpty(line)) continue;

	calculator.ProcessLine(line);
}
while (!reader.EndOfStream);


Console.WriteLine($"Gamma rate: {calculator.GammaRate}");
Console.WriteLine($"Epsilon rate: {calculator.EpsilonRate}");
Console.WriteLine($"Power consumption: {calculator.GammaRate * calculator.EpsilonRate}");


internal sealed class RatesCalculator
{
	private readonly long[] commonBits;

	public RatesCalculator(long bitsCount)
	{
		this.commonBits = new long[bitsCount];
		for (int i = 0; i < bitsCount; ++i) this.commonBits[i] = 0;
	}

	public RatesCalculator ProcessLine(string line)
	{
		if (line?.Length != commonBits.Length) throw new ArgumentException("Wrong line length");

		for (var i = 0; i < line.Length; ++i)
		{
			this.commonBits[i] += line[i] == '1' ? 1 : -1;
		}

		return this;
	}

	public long[] GammaRateBits => this.GetNormalizedBits;
	public long[] EpsilonRateBits => this.GetNormalizedBits.Select(b => b == 1 ? 0L : 1L).ToArray();

	public ulong GammaRate => (ulong) this.GammaRateBits
		.Select((b, i) => b == 0 ? 0 : 1 << commonBits.Length - i - 1)
		.Aggregate(0, (r, n) => r |= n);

	public ulong EpsilonRate => (ulong) this.EpsilonRateBits
		.Select((b, i) => b == 0 ? 0 : 1 << commonBits.Length - i - 1)
		.Aggregate(0, (r, n) => r |= n);

	private long[] GetNormalizedBits => this.commonBits.Select(b => b >= 0 ? 1L : 0L).ToArray();
}
