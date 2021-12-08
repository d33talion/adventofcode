import java.io.File

fun main(args: Array<String>) {
    val measurements = mutableListOf<Int>()

    File("../_input/day01.txt").useLines {
        lines -> lines.forEach {
            measurements.add(it.toInt())
        }
    }

    val increasingIndividualMeasurements = countNumberOfIncreasingValues(measurements)
    println("Amount of increasing measurements: " + increasingIndividualMeasurements)

    val groupsOfMeasurements = splitMeasurementsToGroups(measurements)
    val increasingMeasurementsGroups = countNumberOfIncreasingValues(groupsOfMeasurements)
    println("Amount of increasing measurement groups: " + increasingMeasurementsGroups)
}

fun countNumberOfIncreasingValues(values: List<Int>): Int {
    var qty = 0
    for ((i, v) in values.withIndex()) {
        if (values.size <= i + 1) break

        qty += if (values[i + 1] > v) 1 else 0
    }
    return qty
}

fun splitMeasurementsToGroups(measurements: List<Int>): List<Int> {
    val measurementsGroups = mutableListOf<Int>()
    for ((i, m) in measurements.withIndex()) {
        if (measurements.size < i + 3) break

        measurementsGroups.add(measurements[i] + measurements[i + 1] + measurements[i + 2])
    }
    return measurementsGroups
}
