const fileInput = document.getElementById('fileInput');
const fileName = document.getElementById('fileName');

fileInput.addEventListener('change', () => {
    if (fileInput.files.length > 0) {
        fileName.textContent = fileInput.files[0].name;
    } else {
        fileName.textContent = "No file chosen";
    }
});

document.addEventListener("DOMContentLoaded", () => {
    const getCellValue = (tr, idx) => tr.children[idx].textContent.trim();

    const comparer = (idx, asc) => (a, b) => {
        const v1 = getCellValue(a, idx);
        const v2 = getCellValue(b, idx);

        const n1 = parseFloat(v1.replace(/[^0-9.-]+/g, ""));
        const n2 = parseFloat(v2.replace(/[^0-9.-]+/g, ""));
        if (!isNaN(n1) && !isNaN(n2)) return asc ? n1 - n2 : n2 - n1;

        const dateRegex = /^(\d{2})\.(\d{2})\.(\d{4})$/;
        const m1 = v1.match(dateRegex), m2 = v2.match(dateRegex);
        if (m1 && m2) return asc ? new Date(`${m1[3]}-${m1[2]}-${m1[1]}`) - new Date(`${m2[3]}-${m2[2]}-${m2[1]}`)
            : new Date(`${m2[3]}-${m2[2]}-${m2[1]}`) - new Date(`${m1[3]}-${m1[2]}-${m1[1]}`);

        return asc ? v1.localeCompare(v2) : v2.localeCompare(v1);
    };

    document.querySelectorAll(".contact-table th").forEach(th => {

        if (th.textContent.trim() === "Actions") return;

        th.style.cursor = "pointer";
        th.asc = true;
        th.addEventListener("click", () => {
            th.asc = !th.asc;
            const tbody = th.closest("table").querySelector("tbody");
            const rows = Array.from(tbody.querySelectorAll("tr"));
            const fragment = document.createDocumentFragment();
            rows.sort(comparer([...th.parentNode.children].indexOf(th), th.asc))
                .forEach(tr => fragment.appendChild(tr));
            tbody.appendChild(fragment);
        });
    });
});