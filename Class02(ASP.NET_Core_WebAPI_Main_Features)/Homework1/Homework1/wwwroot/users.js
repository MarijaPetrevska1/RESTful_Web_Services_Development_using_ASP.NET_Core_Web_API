const btnAllUsers = document.getElementById('btnAllUsers');
const btnUserByIndex = document.getElementById('btnUserByIndex');

// Function : Get all users
btnAllUsers.addEventListener('click', async () => {
    btnAllUsers.disabled = true; 
    const ul = document.getElementById('usersList');
    ul.innerHTML = '<li>Loading...</li>'; 

    try {
        const res = await fetch('/api/users'); 
        if (!res.ok) throw new Error(await res.text()); 

        const users = await res.json(); // json
        ul.innerHTML = ''; 
        users.forEach(u => {
            const li = document.createElement('li');
            li.textContent = u;
            ul.appendChild(li);
        });
    } catch (error) {
        ul.innerHTML = `<li style="color:red;">Error: ${error.message}</li>`;
    } finally {
        btnAllUsers.disabled = false; 
    }
});

// === Function: Get one user
btnUserByIndex.addEventListener('click', async () => {
    const index = document.getElementById('userIndex').value; 
    const output = document.getElementById('singleUser');
    btnUserByIndex.disabled = true;
    output.style.color = 'black';
    output.textContent = 'Loading...';

    try {
        const res = await fetch(`/api/users/${index}`); // api with index
        if (!res.ok) throw new Error(await res.text());

        const user = await res.text(); 
        output.style.color = 'green';
        output.textContent = `User: ${user}`;
    } catch (error) {
        output.style.color = 'red';
        output.textContent = `Error: ${error.message}`;
    } finally {
        btnUserByIndex.disabled = false;
    }
});
