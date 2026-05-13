const API_URL = "https://localhost:7035/api"; 
let TOKEN = "";


async function fazerLogin() {
    const user = document.getElementById('usuario').value;
    const pass = document.getElementById('senha').value;

    try {
        const response = await fetch(`${API_URL}/Auth/login`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ usuario: user, senha: pass })
        });

        if (response.ok) {
            const data = await response.json();
            TOKEN = data.token;
            sessionStorage.setItem('token', TOKEN);
            mostrarDashboard();
        } else {
            alert("Erro na autenticação. Verifique as credenciais.");
        }
    } catch (error) {
        console.error("Erro ao conectar na API:", error);
    }
}


async function carregarCursos() {
    const token = sessionStorage.getItem('token');
    
    try {
        const response = await fetch(`${API_URL}/Cursos`, {
            method: 'GET',
            headers: { 
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            }
        });

        if (response.ok) {
            const cursos = await response.json();
            window.listaCursosLocal = cursos; // Guarda para a edição
            const container = document.getElementById('listaCursos');
            
            container.innerHTML = cursos.map(c => `
                <tr>
                    <td>${c.idCurso}</td>
                    <td>${c.nome}</td>
                    <td>${c.cargaHoraria}h</td>
                    <td class="text-center">
                        <button class="btn btn-sm btn-warning fw-bold" onclick="prepararEdicao(${c.idCurso})">EDITAR</button>
                        <button class="btn btn-sm btn-danger fw-bold" onclick="deletarCurso(${c.idCurso})">EXCLUIR</button>
                    </td>
                </tr>
            `).join('');
        }
    } catch (error) {
        console.error("Erro ao carregar cursos:", error);
    }
}


function prepararEdicao(id) {
    const curso = window.listaCursosLocal.find(c => c.idCurso === id);

    if (curso) {
        document.getElementById('cursoId').value = curso.idCurso;
        document.getElementById('nomeCurso').value = curso.nome;
        document.getElementById('cargaHoraria').value = curso.cargaHoraria;
        
        document.getElementById('modalLabel').innerText = "EDITAR CURSO";
        const modal = new bootstrap.Modal(document.getElementById('cursoModal'));
        modal.show();
    }
}


async function salvarCurso() {
    const id = document.getElementById('cursoId').value;
    const token = sessionStorage.getItem('token');

    const curso = {
        idCurso: id ? parseInt(id) : 0,
        nome: document.getElementById('nomeCurso').value,
        cargaHoraria: parseInt(document.getElementById('cargaHoraria').value)
    };

    const metodo = id ? 'PUT' : 'POST';
    const url = id ? `${API_URL}/Cursos/${id}` : `${API_URL}/Cursos`;

    try {
        const response = await fetch(url, {
            method: metodo,
            headers: { 
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(curso)
        });

        if (response.ok) {
            // Fecha o modal corretamente
            const modalElement = document.getElementById('cursoModal');
            const modalInstance = bootstrap.Modal.getInstance(modalElement) || new bootstrap.Modal(modalElement);
            modalInstance.hide();
            
            carregarCursos();
            limparForm();
        }
    } catch (error) {
        alert("Erro ao salvar registro.");
    }
}


async function deletarCurso(id) {
    if (!confirm(`Excluir curso ${id}?`)) return;
    const token = sessionStorage.getItem('token');

    await fetch(`${API_URL}/Cursos/${id}`, {
        method: 'DELETE',
        headers: { 'Authorization': `Bearer ${token}` }
    });
    carregarCursos();
}

// Funções de Interface
function mostrarDashboard() {
    document.getElementById('loginSection').classList.add('d-none');
    document.getElementById('dashboard').classList.remove('d-none');
    document.getElementById('btnLogout').classList.remove('d-none');
    carregarCursos();
}

function limparForm() {
    document.getElementById('cursoId').value = "";
    document.getElementById('nomeCurso').value = "";
    document.getElementById('cargaHoraria').value = "";
    document.getElementById('modalLabel').innerText = "NOVO CURSO";
}

function logout() {
    sessionStorage.clear();
    window.location.reload();
}