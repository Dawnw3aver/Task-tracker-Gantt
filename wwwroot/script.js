google.charts.load('current', {'packages':['gantt']});
    google.charts.setOnLoadCallback(drawChart);

    async function drawChart() {

      var data = new google.visualization.DataTable();
      data.addColumn('string', 'Task ID');
      data.addColumn('string', 'Task Name');
      data.addColumn('string', 'Resource');
      data.addColumn('date', 'Start Date');
      data.addColumn('date', 'End Date');
      data.addColumn('number', 'Duration');
      data.addColumn('number', 'Percent Complete');
      data.addColumn('string', 'Dependencies');

      var rows = await getTasks();
      data.addRows(rows);

      var options = {
        height: 400,
        gantt: {
          trackHeight: 45,
        }
      };

        var chart = new google.visualization.Gantt(document.getElementById('chart_div'));


      chart.draw(data, options);

    }

    async function getTasks() {
      var result = [];
      const response = await fetch("/api/tasks", {
          method: "GET",
          headers: { "Accept": "application/json" }
      });
      if (response.ok === true) {
          const tasks = await response.json();
          tasks.forEach(task => result.push([
            task.id.toString(),
            "ID:" + task.id.toString() + " " + task.taskName,
            task.resource,
            new Date(task.startDate),
            new Date(task.endDate),
            null,
            task.percentComplete,
            null
          ]));
      }
      return result;
  }

    async function createTask(taskName, resource, startDate, endDate, percentComplete) {
  
      const response = await fetch("api/tasks", {
          method: "POST",
          headers: { "Accept": "application/json", "Content-Type": "application/json" },
          body: JSON.stringify({
            taskName: taskName,
            resource: resource,
            startDate: startDate,
            endDate: endDate,
            percentComplete: parseInt(percentComplete)
          })
      });
        if (response.ok === false) {
            const error = await response.json();
            console.log(error.message);
      }
  }

  async function deleteTask(id) {
    const response = await fetch(`/api/tasks/${id}`, {
        method: "DELETE",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        drawChart();
    }
    else {
        const error = await response.json();
        console.log(error.message);
    }
}
    function formReset() {
        document.getElementById("task_name").value =
        document.getElementById("task_resource").value =
        document.getElementById("date_start").value =
        document.getElementById("date_end").value =
        document.getElementById("percent_complete").value = "";
    }

  document.getElementById("create-button").addEventListener("click", async () => {
    const taskName = document.getElementById("task_name").value;
    const taskRresource = document.getElementById("task_resource").value;
    const dateStart = document.getElementById("date_start").value;
    const dateEnd = document.getElementById("date_end").value;
    const percentComplete = document.getElementById("percent_complete").value;
    await createTask(taskName, taskRresource, dateStart, dateEnd, percentComplete);
    formReset();
    drawChart();
  });

  document.getElementById("delete-button").addEventListener("click", async () => {
    const id = parseInt(document.getElementById("delete_task").value);
    document.getElementById("delete-button").reset();
    await deleteTask(id);
  });