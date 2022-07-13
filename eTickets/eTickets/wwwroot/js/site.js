let id = document.getElementById('osid').value
let cfm = document.getElementById('cfm')
let wt = document.getElementById('wt')
let cncl = document.getElementById('cncl')

if (id === 'Confirm') {
    cfm.style = "margin-left:30px;color: currentColor;cursor: not-allowed;opacity: 0.5;text-decoration: none;"
    wt.style = "margin-left:30px;color: currentColor;cursor: not-allowed;opacity: 0.5;text-decoration: none;"
} else if (id === 'Pending') {
    wt.style = "margin-left:30px;color: currentColor;cursor: not-allowed;opacity: 0.5;text-decoration: none;"
} else {
    cfm.style = "margin-left:30px;color: currentColor;cursor: not-allowed;opacity: 0.5;text-decoration: none;"
    wt.style = "margin-left:30px;color: currentColor;cursor: not-allowed;opacity: 0.5;text-decoration: none;"
    cncl.style = "margin-left:30px;color: currentColor;cursor: not-allowed;opacity: 0.5;text-decoration: none;"
}