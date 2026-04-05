
        let startTime = Date.now();
        let timerInterval = setInterval(updateTimer, 1000);

        function updateTimer() {
            const now = Date.now();
            const diff = Math.floor((now - startTime) / 1000);
            const mins = Math.floor(diff / 60).toString().padStart(2, '0');
            const secs = (diff % 60).toString().padStart(2, '0');
            document.getElementById('timer').innerText = `${mins}:${secs}`;
        }

        document.getElementById('game-canvas').addEventListener('click', async function(e) {
            const rect = e.target.getBoundingClientRect();
            const xPercent = ((e.clientX - rect.left) / rect.width) * 100;
            const yPercent = ((e.clientY - rect.top) / rect.height) * 100;

            const now = Date.now();
            const timeInSeconds = Math.floor((now - startTime) / 1000);

            console.log(`TargetX: ${xPercent.toFixed(2)}, TargetY: ${yPercent.toFixed(2)}`);

            showClickMarker(e.clientX - rect.left, e.clientY - rect.top);

            const levelId = Number(document.getElementById('level-id-hidden').value);
            const token = document.querySelector('input[name="__RequestVerificationToken"]').value;

            try {
                const formData = new FormData();
                formData.append('levelId', levelId);
                formData.append('x', xPercent);
                formData.append('y', yPercent);
                formData.append('timeInSeconds', timeInSeconds);
                formData.append('__RequestVerificationToken', token);

                const response = await fetch(`/Levels/CheckClick`, {
                    method: 'POST',
                    body: formData
                });

                const result = await response.json();

                if (result.success) {
                    clearInterval(timerInterval); 

                    const marker = document.getElementById('click-marker');
                    if (marker) marker.style.borderColor = 'lime';

                    Swal.fire({
                        title: 'Поздравления!',
                        html: `<h3 class="text-success">${result.message}</h3>
                               <p class="fs-4">Време: <strong>${document.getElementById('timer').innerText}</strong></p>`,
                        icon: 'success',
                        background: '#1a1a1a',
                        color: '#ffffff',
                        confirmButtonText: 'Към нивата',
                        confirmButtonColor: '#198754',
                        allowOutsideClick: false
                    }).then((swalResult) => {
                        if (swalResult.isConfirmed) {
                            window.location.href = '/Levels/Index';
                        }
                    });
                }

            } catch (error) {
                console.error("Грешка:", error);
            }
        });

        function showClickMarker(x, y) {
            const oldMarker = document.getElementById('click-marker');
            if (oldMarker) oldMarker.remove();

            const marker = document.createElement('div');
            marker.id = 'click-marker';
            marker.style.position = 'absolute';
            marker.style.left = (x - 20) + 'px';
            marker.style.top = (y - 20) + 'px';
            marker.style.width = '40px';
            marker.style.height = '40px';
            marker.style.border = '4px solid red';
            marker.style.borderRadius = '50%';
            marker.style.boxShadow = '0 0 10px white';
            marker.style.pointerEvents = 'none';

            document.querySelector('.game-container').appendChild(marker);
        }

