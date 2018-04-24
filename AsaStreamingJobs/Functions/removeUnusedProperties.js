function main(record) {
    if (record) {
        record.IoTHub && delete record.IoTHub;
        record.PartitionId && delete record.PartitionId;
        record.EventEnqueuedUtcTime && delete record.EventEnqueuedUtcTime;
        record.EventProcessedUtcTime && delete record.EventProcessedUtcTime;
    }
    return record;
}