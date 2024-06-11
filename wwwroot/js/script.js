document.getElementById('transpose-btn').addEventListener('click', async function() {
    const matrixInput = document.getElementById('matrix-input-text').value;
    const matrix = matrixInput.split('\n').map(row => row.split(',').map(Number));
    const response = await fetch('/api/Matrix/transpose', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ Matrix: matrix })
    });
    const result = await response.json();
    document.getElementById('result-text').innerText = JSON.stringify(result);
});

document.getElementById('determinant-btn').addEventListener('click', async function() {
    const matrixInput = document.getElementById('matrix-input-text').value;
    const matrix = matrixInput.split('\n').map(row => row.split(',').map(Number));
    const response = await fetch('/api/Matrix/determinant', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ Matrix: matrix })
    });
    const result = await response.json();
    document.getElementById('result-text').innerText = result;
});

document.getElementById('multiply-btn').addEventListener('click', async function() {
    const matrixAInput = document.getElementById('matrix-a-input').value;
    const matrixBInput = document.getElementById('matrix-b-input').value;
    const matrixA = matrixAInput.split('\n').map(row => row.split(',').map(Number));
    const matrixB = matrixBInput.split('\n').map(row => row.split(',').map(Number));
    const request = { MatrixA: matrixA, MatrixB: matrixB };
    const response = await fetch('/api/Matrix/multiply', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(request)
    });
    const result = await response.json();
    document.getElementById('result-text').innerText = JSON.stringify(result);
});
