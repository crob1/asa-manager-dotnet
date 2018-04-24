// Flatten the aggregates from an array to a record
function main(record) {

    let flatRecord = {
        '__deviceid': record.__deviceid,
        '__ruleid': record.__ruleid
    };

    record.measurements.forEach(function (item) {
        flatRecord[item.measurementname] = {
            'avg': item.avg,
            'max': item.max,
            'min': item.min,
            'count': item.count
        };
    });

    return flatRecord;
}