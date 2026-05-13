const API_URL = "https://localhost:7035/api"
let currentTab = "Alunos";
let editingId = null;

// --- AUTENTICAÇÃO ---
async function login() {
    const user = document.getElementById('username').value;
    const pass = document.getElementById('password').value;

    const response = await fetch(`${API_URL}/Auth/login`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username: user, senha: pass })
    });

    if (response.ok) {
        const data = await response.json();
        localStorage.setItem('token', data.token); // Salva o crachá JWT
        mostrarSistema();
    } else {
        alert("Falha no login!");
    }
}

function logout() {
    localStorage.removeItem('token');
    location.reload();
}

function mostrarSistema() {
    document.getElementById('loginSection').classList.add('hidden');
    document.getElementById('mainSection').classList.remove('hidden');
    document.getElementById('btnLogout').classList.remove('hidden');
    loadTab('Alunos');
}

// --- CRUD GENÉRICO ---
async function loadTab(tab) {
    currentTab = tab;
    document.getElementById('tabTitle').innerText = `Gerenciar ${tab}`;
    const token = localStorage.getItem('token');

    const response = await fetch(`${API_URL}/${tab}`, {
        headers: { 'Authorization': `Bearer ${token}` }
    });

    if (response.status === 401) return logout();
    
    const data = await response.json();
    renderTable(data);
    renderFormFields();
}

function renderTable(data) {
    const tbody = document.getElementById('tableBody');
    const thead = document.getElementById('tableHead');
    tbody.innerHTML = "";

    if (data.length === 0) return;

    // Cabeçalho (Pega as chaves do primeiro objeto)
    const cols = Object.keys(data[0]);
    thead.innerHTML = cols.map(c => `<th>${c}</th>`).join('') + "<th>Ações</th>";

    // Linhas
    data.forEach(item => {
        let rows = cols.map(c => `<td>${item[c]}</td>`).join('');
        tbody.innerHTML += `
            <tr>
                ${rows}
                <td>
                    <button class="btn btn-warning btn-sm" onclick="prepararEdicao(${JSON.stringify(item).replace(/"/g, '&quot;')})">Editar</button>
                    <button class="btn btn-danger btn-sm" onclick="deletar(${item.idAluno || item.idCurso || item.id})">Excluir</button>
                </td>
            </tr>`;
    });
}

function renderFormFields() {
    const container = document.getElementById('formInputs');
    if (currentTab === "Alunos") {
        container.innerHTML = `
            <input type="text" id="inputNome" class="form-control mb-2" placeholder="Nome">
            <input type="text" id="inputSexo" class="form-control mb-2" placeholder="Sexo (M/F)">
            <input type="number" id="inputIdCurso" class="form-control mb-2" placeholder="ID do Curso">
        `;
    }
    // Adicione os else if para os outros controllers aqui
}

async function salvar() {
    const token = localStorage.getItem('token');
    const method = editingId ? 'PUT' : 'POST';
    const url = editingId ? `${API_URL}/${currentTab}/${editingId}` : `${API_URL}/${currentTab}`;

    // Monta o objeto (Ajustar de acordo com a Tab)
    const payload = {
        nome: document.getElementById('inputNome').value,
        sexo: document.getElementById('inputSexo').value,
        idCurso: parseInt(document.getElementById('inputIdCurso').value)
    };
    if(editingId) payload.idAluno = editingId;

    const response = await fetch(url, {
        method: method,
        headers: { 
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify(payload)
    });

    if (response.ok) {
        alert("Sucesso!");
        bootstrap.Modal.getInstance(document.getElementById('modalCadastro')).hide();
        loadTab(currentTab);
    } else {
        alert("Erro ao salvar.");
    }
}

async function deletar(id) {
    if (!confirm("Deseja realmente excluir?")) return;
    const token = localStorage.getItem('token');

    await fetch(`${API_URL}/${currentTab}/${id}`, {
        method: 'DELETE',
        headers: { 'Authorization': `Bearer ${token}` }
    });
    loadTab(currentTab);
}

function prepararEdicao(item) {
    editingId = item.idAluno || item.idCurso || item.id;
    document.getElementById('inputNome').value = item.nome;
    document.getElementById('inputSexo').value = item.sexo;
    document.getElementById('inputIdCurso').value = item.idCurso;
    new bootstrap.Modal(document.getElementById('modalCadastro')).show();
}

function limparForm() {
    editingId = null;
    const inputs = document.querySelectorAll('#formInputs input');
    inputs.forEach(i => i.value = "");
}

// Verifica se já está logado ao abrir a página
if(localStorage.getItem('token')) mostrarSistema();