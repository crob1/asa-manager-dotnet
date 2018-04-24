// Apply rule filter on the incoming record
function main(record) {
    let ruleFunction = new Function('record', record.__rulefilterjs);
    return ruleFunction(record);
}